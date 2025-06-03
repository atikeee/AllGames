from flask import request, render_template, redirect, abort, send_from_directory
from datetime import datetime
from storage import buzzer_entries, name_locks
import csv, os, re
import random
from PIL import Image
from io import BytesIO
import base64


def is_request_from_localhost():
    return request.remote_addr in ("127.0.0.1", "localhost", "::1")

def find_image_pairs(folder):
    files = os.listdir(folder)
    bases = set()
    for f in files:
        if f.endswith("1.jpg"):
            base = f[:-5]
            if f"{base}2.jpg" in files:
                bases.add(base)
    return list(bases)

def scramble_image(image_path, n):
    image = Image.open(image_path)
    width, height = image.size
    tile_w, tile_h = width // n, height // n
    tiles = []

    for i in range(n):
        for j in range(n):
            box = (j * tile_w, i * tile_h, (j + 1) * tile_w, (i + 1) * tile_h)
            tiles.append(image.crop(box))

    random.shuffle(tiles)
    new_img = Image.new('RGB', (width, height))
    for idx, tile in enumerate(tiles):
        i, j = divmod(idx, n)
        new_img.paste(tile, (j * tile_w, i * tile_h))

    buf = BytesIO()
    new_img.save(buf, format='PNG')
    return base64.b64encode(buf.getvalue()).decode()

def load_clips():
    clips = []
    with open("clips.csv", newline='') as csvfile:
        reader = csv.reader(csvfile)
        for row in reader:
            url = row[0]
            segments = [(int(row[i]), int(row[i+1])) for i in range(1, len(row), 2)]
            clips.append({"url": url, "segments": segments})
    return clips

def extract_video_id(url):
    match = re.search(r"v=([^&]+)", url)
    return match.group(1) if match else None


def configure_routes(app):
    @app.route('/', methods=['GET', 'POST'])
    def buzzer():
        message = ''
        ip = request.remote_addr
        name_value = name_locks.get(ip, '')
        name_locked = bool(name_value)

        if request.method == 'POST':
            name = request.form.get('name')
            note = request.form.get('note', '')
            time = datetime.now().strftime("%Y-%m-%d %H:%M:%S")

            if not name_locked:
                name_locks[ip] = name
                name_value = name
                name_locked = True

            buzzer_entries.append({'name': name_value, 'ip': ip, 'time': time, 'note': note})
            message = f"Buzz from {name_value} recorded!"

        return render_template("buzzer.html", message=message, name=name_value, name_locked=name_locked, note='')

    @app.route('/report', methods=['GET', 'POST'])
    def report():
        if request.method == 'POST':
            buzzer_entries.clear()
        return render_template("report.html", entries=buzzer_entries)

    @app.route('/resetnames', methods=['POST'])
    def reset_names():
        buzzer_entries.clear()
        name_locks.clear()
        return redirect('/report')
    @app.route('/shuffle', methods=['POST'])
    def shuffle_names():
        unique_names = list(set([entry['name'] for entry in buzzer_entries]))
        from random import shuffle
        shuffle(unique_names)
        return render_template("report.html", entries=buzzer_entries, unique_names=unique_names)
    
    @app.route('/photoscramble', methods=['GET', 'POST'])
    def photoscramble():
        if not is_request_from_localhost():
            abort(403)  # Forbidden
        photo_folder = 'photos'
        image_list = sorted([f for f in os.listdir(photo_folder) if f.lower().endswith(('png', 'jpg', 'jpeg'))])
        index = int(request.args.get('index', 0))
        n = int(request.form.get('grid_size', request.args.get('n', 20)))

        if index < 0:
            index = 0
        if index >= len(image_list):
            index = len(image_list) - 1

        image_path = os.path.join(photo_folder, image_list[index])
        scrambled = scramble_image(image_path, n)

        return render_template("photoscramble.html",
                               image_data=scrambled,
                               index=index,
                               n=n,
                               total=len(image_list),
                               has_prev=index > 0,
                               has_next=index < len(image_list) - 1)
    @app.route('/guesstune')
    def guesstune():
        clips = load_clips()
        clip_index = int(request.args.get("clip", 0))
        segment_index = int(request.args.get("seg", 0))

        clip_index = max(0, min(clip_index, len(clips) - 1))
        segment_index = max(0, segment_index)

        clip = clips[clip_index]
        video_id = extract_video_id(clip["url"])
        segments = clip["segments"]
        segment = segments[segment_index % len(segments)]

        return render_template("guesstune.html",
                               video_id=video_id,
                               start=segment[0],
                               end=segment[1],
                               clip_index=clip_index,
                               segment_index=segment_index,
                               total_clips=len(clips),
                               total_segments=len(segments))
    @app.route('/photopair', methods=['GET'])
    def photopair():
        m = int(request.args.get("m", 4))
        n = int(request.args.get("n", 4))
        delay = int(request.args.get("delay", 2000))
        folder = "photopair"
        pairs = find_image_pairs(folder)
        needed = (m * n) // 2
        if len(pairs) < needed:
            images = []
        else:
            selected = random.sample(pairs, needed)
            images = []
            for base in selected:
                images.append(f"{base}1.jpg")
                images.append(f"{base}2.jpg")
            random.shuffle(images)

        return render_template("photopair.html", m=m, n=n, delay=delay, images=images)
    from flask import send_from_directory

    @app.route('/photopair_images/<filename>')
    def photopair_image(filename):
        return send_from_directory("photopair", filename)