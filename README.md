# HTPC-Launcher
A little WPF windows application to execute your HTPC application.

Launch applications like Plex, Steam, Emulation Station and others without need to launch them from the windows menu.

# Usage

* Compile the program.
* Edit the configuration.cfg in the program folder.
* Run the program.

# Configuration

* The configuration.cfg is a json containing all the settings.

```
{
  "BackgroundImagePath": "[Path to the background image]",
  "AppButtons": [
    {
      "Name": "[Name of the application]",
      "Path": "[Path]",
      "WorkingPath": "[Working Path]",
      "Parameters": "[Opcional Parameters]",
      "IconPath": "[Path to the icon of the applicacion]"
    }
  ],
  "Fullscreen": true // If the applicacion starts in fullscreen
}
```

# Configuration Examples

```
{
  "BackgroundImagePath": "c:/htpc-laucher/background.png",
  "AppButtons": [
    {
      "Name": "Steam",
      "Path": "c:/games/steam/steam.exe",
      "WorkingPath": "c:/games/steam",
      "Parameters": "",
      "IconPath": "c:/icons/steam.ico"
    }
  ],
  "Fullscreen": true
}
```
