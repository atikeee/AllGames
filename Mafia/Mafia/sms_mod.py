def sendsmsfun(sms,recipientlist):
    import smtplib
    from email.mime.text import MIMEText
    # username="atikeee@gmail.com" #SENDER email address 
    username="sd.gcf.rf@gmail.com" #SENDER email address 
    password="SanDiego123$" #SENDER email address pwd
    # password="asdqwe123!@#" #SENDER email address pwd
    server = smtplib.SMTP("smtp.gmail.com", 587)
    server.ehlo()
    server.starttls()
    try:
            server.login(username,password)
            server.set_debuglevel(1)
            #msg = MIMEText("""Test SMS""")
            msg = MIMEText(sms)
            # sender = 'atikeee@gmail.com'
            sender = username
            # msg['Subject'] = "Test SMS: HELLO OFFICE FOR TESTING"
            # msg['From'] = sender
            # msg['To'] = ", ".join(recipientlist)
            msg['To'] = recipientlist
            server.sendmail(sender, recipientlist, msg.as_string())
            server.quit()
            print("Successfully sent SMS")
            #print recipients
    except Exception as e:
       print(e)
    return;  
#U.S. Carrier            SMS Gateway                 MMS Gateway
#Altel                   @sms.alltelwireless.com     @mms.alltelwireless.com
#AT&T                    @txt.att.net                @mms.att.net
#Boost Mobile            @sms.myboostmobile.com      @myboostmobile.com
#Sprint                  @messaging.sprintpcs.com    @pm.sprint.com
#T-Mobile                @tmomail.net                @tmomail.net
#U.S. Cellular           @email.uscc.net             @mms.uscc.net
#Verizon                 @vtext.com                  @vzwpix.com
#Virgin Mobile           @vmobl.com                  @vmpix.com
    
    
    
# recipients = ['8583780631@vtext.com']   
# recipients = ['7606383563@tmomail.net']
# recipients = ['4083062628@txt.att.net']

#recipient = '8582264592@txt.att.net'
#
## msg = 'you are \n !!!MAFIA!!!' 
#msg = 'if you get this msg reply to me(atiq)' 
#sendsmsfun(msg,recipient)