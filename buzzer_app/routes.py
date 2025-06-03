from flask import request, render_template, redirect, make_response
from datetime import datetime
from storage import buzzer_entries

def configure_routes(app):
    @app.route('/', methods=['GET', 'POST'])
    def buzzer():
        message = ''
        name_value = request.cookies.get('name', '')
        name_locked = False

        if request.method == 'POST':
            name = request.form.get('name')
            ip = request.remote_addr
            time = datetime.now().strftime("%Y-%m-%d %H:%M:%S")

            # Save name to cookie if not already stored
            if not name_value:
                name_value = name
                name_locked = True
            else:
                name_locked = True  # name already exists in cookie

            note = request.form.get('note', '')
            buzzer_entries.append({'name': name_value, 'ip': ip, 'time': time, 'note': note})

            message = f"Buzz from {name_value} recorded!"

            resp = make_response(render_template("buzzer.html", message=message, name=name_value, name_locked=name_locked, note=note))
            resp.set_cookie('name', name_value)
            return resp

        if name_value:
            name_locked = True

        return render_template("buzzer.html", message=message, name=name_value, name_locked=name_locked, note="")
    @app.route('/report', methods=['GET', 'POST'])
    def report():
        if request.method == 'POST':
            buzzer_entries.clear()
            return redirect('/report')

        return render_template("report.html", entries=buzzer_entries)
    @app.route('/resetnames', methods=['POST'])
    def reset_names():
        # This only affects cookie-based IP-to-name locks
        # You can't truly delete cookies on other users' browsers, but we'll reset the entries
        buzzer_entries.clear()
        resp = make_response(redirect('/report'))
        resp.set_cookie('name', '', expires=0)
        #return redirect('/report')
        return resp