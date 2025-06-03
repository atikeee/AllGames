from flask import request, render_template, redirect
from datetime import datetime
from storage import buzzer_entries, name_locks
import os
import random
from PIL import Image
from io import BytesIO
import base64

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
