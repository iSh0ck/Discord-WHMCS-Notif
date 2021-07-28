# Functionnalities
- The bot can do:
  - Checking opening ticket
  - Checking opening in waiting for anwser
  - Checking pending order
    - Can know if an order is paid
    - Can know if an order is in waiting for payment or unpaid
  - Check interval is 15mn and can be edited

# Prerequires
- First of all you will need to get .Net Core 5.0
- In second time you will need to update credential into the code in these two files:
  - Discord.cs
  - Program.cs
- In Discord.cs you will need to update the differents ID of channels & server
- In Program.cs you will need to update the differents ID of group permission to get notified

### Dotnet & Linux
I'm using this bot on Linux so i will explain you how you can install .NET on Linux (Debian 10)

1. Add Microsoft package
```BASH
wget https://packages.microsoft.com/config/debian/10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
```
2. Install the SDK
```BASH
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-5.0
```
3. Install the runtime
```BASH
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y aspnetcore-runtime-5.0
```
> All information about the installation of .NET on Linux can be found here: https://docs.microsoft.com/en-us/dotnet/core/install/linux-debian

# Screenshots
![Image of tickets](http://image.noelshack.com/fichiers/2021/30/1/1627295128-discord-4ltpgsj2nb.png)
![Image of orders](http://image.noelshack.com/fichiers/2021/30/1/1627295061-discord-lht2ulct2m.png)
