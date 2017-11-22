# TNTScaleService

Windows Service that interfaces with HID compliant Mettler Toledo scales and sends that data to a webservice that will send the data via a websocket to the apprpriate field on the trollandtoad.com website. 

InstallUtil.exe should be used to install the service per the standard. 

This service has in the install directory a config file that allows for the setting of many different things to include, url of service to send data to, the frequency of checks for weight changes on the scale, the enabling or disabling of debug mode and the paths for log files that will be generated if debug is on. 

The service checks the scale as frequently as you tell it to, but it only sends data to the service if the weight has changed from the last weight that was sent. 

--Jeremy Bond
