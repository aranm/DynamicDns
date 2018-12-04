H1 Free Dynamic DNS Service

If you don't have a pay for a fixed IP Address and want to know your IP address so you can connect to your home computer there are a few services that you can pay for that run in the background. These run on your home PC and periodically check your IP address and update a domain name registry so that even though your IP address changes the domain name changes the IP address it is pointing to. Domain names have a TTL (Time to Live) value, so the services set this value to its minimum value and every time your home IP address changes they update the domain name to point to the new IP address.

I was paying for a service like this so I could connect to my home computer via remote desktop as the service was cheaper than getting a fixed IP address from my ISP. Lately the service expired and instead of paying the renewal fee I wrote this little bit of software so that I could avoid the fees.

H3 How It Works:

The system is comprised of two main parts, a web server that displays the latest IP address of your home computer and a Windows application that stores the IP addresses when they change. The web server also has a small REST API (One method) that the Windows application calls to get the IP address of the calling computer.

1. Windows application calls REST API -> REST API returns the IP Address of the calling computer
2. Windows application stores that IP Address in table storage
3. Web application displays the IP address and has a download link for an RDP file to allow for easy connection to the Windows machine

H3 Installation

The system must be installed in the following manner:
1. Create an Azure Storage account
2. Create an Azure Web Site to host **Dynamic.Dns.Web**
3. Update the application key *StorageConnectionString* in the web.config of **Dynamic.Dns.Web**
4. Install the **Dynamic.Dns.Web** project on the Azure Web Site, you can use Web Publish directly from Visual Studio
5. Install the project **Dynamic.Dns.UserInterface** and make it run when Windows starts. [Have a look here](https://support.microsoft.com/en-au/help/4026268/windows-10-change-startup-apps) for information about how to run an application automatically. You will also have to set the following application keys in the [App.config file](https://github.com/aranm/DynamicDns/blob/master/src/Dynamic.Dns.UserInterface/App.config):

```xml
  <appSettings>
    <add key="BaseAddressUri" value="Web site you created in Step 2"/>
    <add key="AddressPath" value="api/v1/address"/>
    <add key="StorageConnectionString" value="Storage connection string you created in Step 1" />
  </appSettings>
```
- BaseAddressUri - This is the URL of the web site that you publish the **Dynamic.Dns.Web** site to
- AddressPath - You can leave this as is, unless you change the REST API address
- StorageConnectionString - the connection string of the Azure Storage account that stores the IP addresses that get updated when the IP address changes

H3 Usage

The system checks for a change in IP address every 15 minutes, or you can open the application by double clicking on the icon in the system tray and cliking "Refresh". It also shows you the current IP address of your computer as well. the main page of the web site shows you the current IP address and has a Remote Desktop file available to download so you can go to the wen site, download the RDP file and connect directly to your home PC. Or you can use the IP address to connect to your home web server or whatever else you need your IP address for.

H3 TODO

The code was thrown together in a couple of hours, it is a bit rough, I would welcome some pull requests. The system has no authentication, if anyone knows the URL of the web site they can get your home IP address. It could do with some basic authentication being created. The code also doesn't handle exceptions and could do with some work there.

H3 Enjoy.

I hope this saves some people out there some money.