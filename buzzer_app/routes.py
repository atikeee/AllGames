import pandas as pd
from flask import request, render_template, redirect, abort, send_from_directory, make_response,url_for,jsonify
from datetime import datetime
from storage import *
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
def generate_letter_mapping():
    import random, string
    letters = list(string.ascii_uppercase)
    while True:
        shuffled = letters[:]
        random.shuffle(shuffled)
        if all(l != s for l, s in zip(letters, shuffled)):
            return dict(zip(letters, shuffled))

def configure_routes(app,socketio):
    @app.route("/")
    def index():
        return render_template("index.html")

   
    @app.route("/buzzer", methods=["GET", "POST"])
    def buzzer():
        message = ''
        name_value = request.cookies.get('name', '')
        name_locked = request.cookies.get('name_locked')
        #name_locked = name_locked_cookie == 'true'

        if request.method == 'POST':
            if not name_locked:
                name = request.form.get('name')
                ip = request.remote_addr
                time = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
                note = request.form.get('note')
                message = f"Buzz from {name} recorded!"
                buzzer_entries.append({'name': name, 'ip': ip, 'note': note, 'time': time})
                resp = make_response(render_template("buzzer.html", message=message, name=name, name_locked=True, note=""))
                
                resp.set_cookie('name', name)
                resp.set_cookie('name_locked', 'true')
                return resp
            else:
                name = name_value
                note = request.form.get('note')
                ip = request.remote_addr
                time = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
                message = f"Buzz from {name} recorded!"
                buzzer_entries.append({'name': name, 'ip': ip, 'note': note, 'time': time})
                return render_template("buzzer.html", message=message, name=name, name_locked=True, note=note)

        return render_template("buzzer.html", message=message, name=name_value, name_locked=name_locked, note="")




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
        m = int(request.args.get("m", 3))
        n = int(request.args.get("n", 8))
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
    @app.route('/misc_image/<filename>')
    def misc_image(filename):
        return send_from_directory('misc', filename)
    @app.route('/misc')
    def misc():
        index = int(request.args.get("index", 0))
        csv_path = "misc_data.csv"
        if not os.path.exists(csv_path):
            return "CSV file not found."

        df = pd.read_csv(csv_path).fillna("")
        index = max(0, min(index, len(df) - 1))
        data = df.iloc[index].to_dict()
        data["index"] = index
        data["total"] = len(df)
        return render_template("misc.html", data=data)
    @app.route('/riddle')
    def riddle():
        import glob

        index = int(request.args.get("index", 0))
        files = sorted(glob.glob("riddle/q*.txt"))

        if not files:
            return "No riddle files found."

        index = max(0, min(index, len(files) - 1))

        with open(files[index], 'r', encoding='utf-8') as f:
            parts = f.read().split("###")
            question = parts[0].strip() if len(parts) > 0 else ""
            answer = parts[1].strip() if len(parts) > 1 else ""
            hint = parts[2].strip() if len(parts) > 2 else ""

        data = {
            "question": question,
            "answer": answer,
            "hint": hint,
            "index": index,
            "total": len(files)
        }

        return render_template("riddle.html", data=data)
    @app.route('/crack')
    def crack():
        riddle_dir = 'riddle'
        files = sorted(f for f in os.listdir(riddle_dir) if f.startswith('s') and f.endswith('.txt'))

        def encode(text, mapping):
            return ''.join(mapping.get(ch.upper(), ch) for ch in text)

        mapping = generate_letter_mapping()
        data = []

        for fname in files:
            with open(os.path.join(riddle_dir, fname), encoding='utf-8') as f:
                answer = f.read().strip().upper()
                cipher = encode(answer, mapping)
                data.append({'question': cipher, 'answer': answer})

        return render_template('crack.html', data=data)
    @app.route("/codenames")
    def codenames():
        last_team = hint_log[-1][0] if hint_log else "Red"

        if not current_game['words']:  # Only if game not started
            return redirect(url_for('start_codenames'))

        return render_template(
            "codenames.html",
            words=current_game['words'],
            colors=current_game['colors'],
            last_team=last_team,
            hint_log=hint_log,
            revealed=current_game.get('revealed', set()),  # ✅ Add this line
            winner=current_game.get('winner')
        )
    @app.route("/codenames_spy", methods=["GET", "POST"])
    def codenames_spy():
        global hint_log

        if not current_game['words']:  # fallback safety
            return redirect(url_for('start_codenames'))

        if request.method == "POST":
            hint = request.form.get("hint")
            count = request.form.get("count")
            hint_log.append([current_game['team'], hint, count])  # updated structure
            current_game['team'] = "blue" if current_game['team'] == "red" else "red"
            if hint:
                socketio.emit('new_hint',{'hint':hint, 'count':count})
        return render_template(
            "codenames_spy.html",
            words=current_game['words'],
            colors=current_game['colors'],
            hint_log=hint_log,
            current_team=current_game['team'],
            zip=zip
        )

    @app.route("/start_codenames")
    def start_codenames():
        global hint_log
        word_file = 'words.txt'
        with open(word_file) as f:
            all_words = [line.strip() for line in f if line.strip()]
        selected_words = random.sample(all_words, 25)

        color_list = ['red'] * 9 + ['blue'] * 8 + ['black'] + ['gray'] * 7
        random.shuffle(color_list)

        current_game['words'] = selected_words
        current_game['colors'] = color_list
        current_game['revealed'] = set()
        current_game['team'] = 'red'
        current_game['winner'] = None
        hint_log.clear()
        socketio.emit("new_game")
        return redirect(url_for('codenames'))
    
    @app.route("/reveal/<int:index>", methods=["POST"])
    def reveal_word(index):
        current_game['revealed'].add(index)
        red_revealed = sum(1 for i in current_game['revealed'] if current_game['colors'][i] == 'red')
        blue_revealed = sum(1 for i in current_game['revealed'] if current_game['colors'][i] == 'blue')
        black_revealed = any(current_game['colors'][i] == 'black' for i in current_game['revealed'])
        winner = None
        if black_revealed:
            winner = "Blue" if hint_log and hint_log[-1][0] == "red" else "Red"
        elif red_revealed == 9:
            winner = "Red"
        elif blue_revealed == 8:
            winner = "Blue"

        current_game['winner'] = winner  # ✅ Save winner
        return jsonify({"winner": winner})
        #return redirect(url_for('codenames'))
        #return '', 204  # no content
    @app.route("/panchforon/namelist", methods=["GET", "POST"])
    def panchforon_namelist():
        global pf_players, pf_deck
        pf_words_file = 'pf_words.txt'

        message = ""

        if request.method == "POST":
            action = request.form.get("action")
            if action == "add":
                name = request.form.get("player")
                if name and name not in pf_players:
                    pf_players.append(name)
                else:
                    message = "Name already exists or is empty."
             

            elif action == "clear":
                pf_players.clear()
            elif action == "delete":
                name = request.form.get("name_to_del")
                if name in pf_players:
                    pf_players.remove(name)
            elif action == "randomize":
                random.shuffle(pf_players)
            elif action == "start":
                try:
                    with open(pf_words_file) as f:
                        words = [line.strip() for line in f if line.strip()]
                    pf_deck = random.sample(words, len(pf_players) * 3)  # example multiplier
                    
                    return redirect(url_for("panchforon_play"))
                except FileNotFoundError:
                    message = "Word file not found."
        return render_template("panchforon_namelist.html", pf_players=pf_players)

    @app.route("/panchforon/play")
    def panchforon_play():
        if not pf_players:
            return redirect(url_for('panchforon_namelist'))  # fallback if no players or deck

        
        timer_value = pf_timer[pf_level-1]
        return render_template("play.html",
                           pf_level=pf_level,
                           pf_players=pf_players,
                           pf_player_idx=pf_player_idx,
                           timer=timer_value,
                           pf_deck=pf_deck,
                           pf_word_idx = pf_word_idx,
                           pf_cards=pf_cards)

    @app.route("/panchforon/next_player", methods=["POST"])
    def next_player():
        global pf_player_idx, pf_cards, pf_deck
        data = request.get_json()
        if not data:
            return jsonify({"error": "No data received"}), 400

        pf_cards = data.get("pf_cards", {})
        pf_deck = data.get("pf_deck", [])

        pf_player_idx = (pf_player_idx + 1) % len(pf_players)
        socketio.emit("update_progress")

        return jsonify({"status": "success"})
        
    @app.route("/panchforon/next_level", methods=["POST"])
    def panchforon_next_level():
        global pf_players, pf_deck, pf_level,pf_cards, pf_word_idx
        print("before pf players",pf_players);
        print("before pf decks",pf_deck);
        print("before pf cards",pf_cards);
        pf_word_idx = 0
        # 1. Clear pf_players
        all_words = []
        for player in pf_players:
            if(player in pf_cards):
                all_words.extend(pf_cards[player])  # Combine all words
        pf_deck = all_words

        pf_cards.clear()  # Clear player dictionary

        # 2. Increment pf_level
        pf_level += 1
        print("after pf players",pf_players);
        print("after pf decks",pf_deck);
        print("affore pf cards",pf_cards);
        socketio.emit("update_result")

        return jsonify({"status": "ok", "pf_level": pf_level, "pf_deck": pf_deck})
    @app.route("/panchforon/status")
    def panchforon_status():
        
        
        # Progress Table: transpose words into columns
        max_len = max((len(v) for v in pf_cards.values()), default=0)
        progress_rows = []
        for i in range(max_len):
            row = []
            for player in pf_players:
                row.append(pf_cards.get(player, [])[i] if i < len(pf_cards.get(player, [])) else "")
            progress_rows.append(row)

        # Result Table
        result_data = {p: [0, 0, 0, 0] for p in pf_players}  # [lvl1, lvl2, lvl3, total]
        for player, words in pf_cards.items():
            for word in words:
                result_data[player][pf_level - 1] += 1  # count current level
            result_data[player][3] = sum(result_data[player][:3])

        return render_template("status.html",
                            players=pf_players,
                            progress_rows=progress_rows,
                            result_data=result_data)
