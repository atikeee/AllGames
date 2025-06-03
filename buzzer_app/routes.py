from flask import request, render_template, redirect
from datetime import datetime
from storage import buzzer_entries, name_locks

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