from flask import request, render_template, redirect, make_response
from datetime import datetime
from storage import buzzer_entries

def configure_routes(app):
    @app.route('/', methods=['GET', 'POST'])
    def buzzer():
        message = ''
        name_value = request.cookies.get('name', '')

        if request.method == 'POST':
            name = request.form.get('name')
            ip = request.remote_addr
            time = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
            buzzer_entries.append({'name': name, 'ip': ip, 'time': time})
            message = f"Buzz from {name} recorded!"
            resp = make_response(render_template("buzzer.html", message=message, name=name))
            resp.set_cookie('name', name)
            return resp

        return render_template("buzzer.html", message=message, name=name_value)

    @app.route('/report', methods=['GET', 'POST'])
    def report():
        if request.method == 'POST':
            buzzer_entries.clear()
            return redirect('/report')

        return render_template("report.html", entries=buzzer_entries)
