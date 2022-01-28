# Dell-R720-Fan-Controller

### Project: Fan Controller Application for PowerEdge R720 <img alt="" align="right" src="https://img.shields.io/badge/Status-Prototype%20Phase-informational?style=flat&logoColor=white&color=73398D" />


<!-- Repo Cover Image -->
<p style="background-color:rgba(22,22,22,1.00)" align="center">
<img align="center" src="https://github.com/CrashOverrideProductions/Dell-R720-Fan-Controller/blob/main/images/R720.jpg?raw=true" />
</p>

<!-- Repo Stats -->
<img align="center" src="https://img.shields.io/github/commit-activity/m/CrashOverrideProductions/Dell-R720-Fan-Controller"> <img align="center" src="https://img.shields.io/github/last-commit/CrashOverrideProductions/Dell-R720-Fan-Controller"> <img align="center" src="https://img.shields.io/github/languages/code-size/CrashOverrideProductions/Dell-R720-Fan-Controller"> <img align="center" src="https://img.shields.io/github/directory-file-count/CrashOverrideProductions/Dell-R720-Fan-Controller">

### Details
something something darkside


---
<!-- To Do List -->
### To Do List
- [ ] Complete Readme.md
- [ ] Develop Fan Speed Curve Algorithim
- [ ] Design Windows Service
- [ ] Design Windows Settings UI

------------
### CMD Commands
print temps and fans rpms
ipmitool -I lanplus -H youripaddresshere -U root -P calvin sensor reading "Ambient Temp" "FAN 1 RPM" "FAN 2 RPM" "FAN 3 RPM"

print fan info
ipmitool -I lanplus -H youripaddresshere -U root -P calvin  sdr get "FAN 1 RPM" "FAN 2 RPM" "FAN 3 RPM"

enable manual/static fan control
ipmitool -I lanplus -H youripaddresshere -U root -P calvin  raw 0x30 0x30 0x01 0x00

disable manual/static fan control
ipmitool -I lanplus -H youripaddresshere -U root -P calvin  raw 0x30 0x30 0x01 0x01

Set Fan Speed (Last Byte)
ipmitool -I lanplus -H youripaddresshere -U root -P calvin  raw 0x30 0x30 0x02 0xff 0x00

Fan Speed Range
0x00 = 000%
0x14 = 020%
0x1E = 030%
0x2D = 045%
0x64 = 100%

------------
### Process




<!-- Licencing Always at the Bottom -->
------------
### Licencing <img alt="" align="right" src="https://img.shields.io/badge/Licence-CC--BY--NC--SA--4.0-informational?style=flat&logo=Creative%20Commons&logoColor=white&color=EF9421" />

**Creative Commons: Attribution - NonCommercial - ShareAlike 4.0 International (CC BY-NC-SA 4.0)**


**You are free to:**

**Share** — copy and redistribute the material in any medium or format

**Adapt** — remix, transform, and build upon the material


**Under the following terms:**

**Attribution** — You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.

**NonCommercial** — You may not use the material for commercial purposes.

**ShareAlike** — If you remix, transform, or build upon the material, you must distribute your contributions under the same license as the original.

No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.
