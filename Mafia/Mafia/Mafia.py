from random import shuffle
from sms_mod import sendsmsfun
import smtplib, os
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
import sys
import contextlib
import cStringIO

#@contextlib.contextmanager
#def nostdout():
#    save_stdout = sys.stdout
#    sys.stdout = cStringIO.StringIO()
#    yield
#    sys.stdout = save_stdout
    

#
def send_email(body,recipient_list):
    sender              = 'detectivemafia@gmail.com'
    password            =   "asdqwe123!@#"
    # sender              = 'sd.gcf.rf@gmail.com'
    # password            =   "SanDiego123$"
    try:
        server = smtplib.SMTP("smtp.gmail.com", 587)
        server.ehlo()
        server.starttls()
        server.login(sender, password)
        server.set_debuglevel(1)
        msg = MIMEMultipart('alternative')
        part = MIMEText(body, 'html')
        msg.attach(part)   
        msg['Subject'] = 'Game'
        msg['From'] = sender
        msg['To'] = ", ".join(recipient_list)
        server.sendmail(sender, recipient_list, msg.as_string())
    except Exception as E:
        pass
        # print(str(E))
         
#
def main():
# 0 sms 1 email
    
    listofpeople=[
    [0,'Atiq','8582264592@txt.att.net'],
    #[1,'Atiq','atikeee@gmail.com'],
    # [0,'Tarek','6038345408@tmomail.net'],
    #[0,'Tanim','5154517346@messaging.sprintpcs.com'],
    #[0,'Eshita','3195383674@messaging.sprintpcs.com'],
    # [0,'Mokarram','8326208002@tmomail.net'],
    # [0,'Juni','4692269443@tmomail.net'],
    [0,'Ila','8583780631@vtext.com'],
    [0,'Sazzad','9518233471@txt.att.net'],
    [0,'Lupa','9518233472@txt.att.net'],
    # [0,'Sumon','7016304244@txt.att.net'],
    [0,'Sumon','8582657932@txt.att.net'],
    # [0,'Rubait','2142296542@txt.att.net'],
    [0,'Eva','8584379394@tmomail.net'],
    [0,'Jewel','8586106747@txt.att.net'],
    [0,'Misha','8582753464@txt.att.net'],
    # [0,'Uccash','7606383563@tmomail.net'],
    [1,'Tuli','tanhatuly@gmail.com'],
    [0,'Rabbi','8582269070@txt.att.net'],
    [0,'Turin','9126016186@tmomail.net'],
    [0,'Simul','4083062628@txt.att.net'],
    [0,'Shatila','5189612034@txt.att.net'],
    
    ]
    listofrole = ['' for x in range(len(listofpeople))]
    listofrole[0] = 'MAFIA'
    listofrole[1] = 'DETECTIVE'
    listofrole[2] = 'DOCTOR'
    # listofrole[3] = 'MODERATOR'
    shuffle(listofrole)
    moderatormsg = ''
    for i, people in enumerate(listofpeople):
        if listofrole[i] =='MAFIA':
            moderatormsg+= 'MAFIA:'+people[1]+'\n'
        elif listofrole[i] =='DETECTIVE':
            moderatormsg+= 'DETECTIVE:'+people[1]+'\n'
        elif listofrole[i] =='DOCTOR':
            moderatormsg+= 'DOCTOR:'+people[1]+'\n'
    # print randomizelist
    # print listofrole
    for i, people in enumerate(listofpeople):
        if listofrole[i] =='MAFIA':
            msg = '!!!Mafia!!!'
            # print msg,people
            if people[0]:
                send_email(msg,[people[2]])
            else:
                sendsmsfun(msg,people[2])
        elif listofrole[i] =='DETECTIVE':
            msg= 'Detective'
            # print msg,people
            if people[0]:
                send_email(msg,[people[2]])
            else:
                sendsmsfun(msg,people[2])
        elif listofrole[i] =='DOCTOR':
            msg= 'Doctor'
            # print msg,people
            if people[0]:
                send_email(msg,[people[2]])
            else:
                sendsmsfun(msg,people[2])
    
        elif listofrole[i] =='MODERATOR':
            # msg= 'Moderator:'+people+'\n'
            msg = '++You are Moderator++\n'
            # print msg,people
            if people[0]:
                send_email(msg,[people[2]])
            else:
                sendsmsfun(msg,people[2])
        else:
            msg= 'Villager'
            if people[0]:
                send_email(msg,[people[2]])
            else:
                sendsmsfun(msg,people[2])
            # print msg,people
if __name__=='__main__':
    # sys.stdout = os.devnull
    sys.stderr = os.devnull
    main()