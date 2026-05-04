# DualOS

DualOS és un sistema operatiu basat en COSMOS, pensat com un projecte acadèmic per explorar el desenvolupament d’un sistema operatiu modern en .NET i C#. El projecte neix amb l’objectiu de construir una base ordenada, visual i escalable sobre la qual es puguin anar afegint funcionalitats pròpies d’un sistema operatiu.

Més enllà de la part tècnica, DualOS també busca oferir una identitat pròpia, una estructura clara i una experiència coherent, de manera que serveixi tant per aprendre conceptes de baix nivell i arquitectura de sistemes com per practicar bones bases de desenvolupament en C#.

## Descripció del SO

DualOS és un sistema operatiu en desenvolupament orientat a l’aprenentatge, la pràctica i l’experimentació. Està construït sobre COSMOS, fet que permet treballar en un entorn proper al desenvolupament real de sistemes operatius utilitzant tecnologies conegudes de l’ecosistema .NET.

L’objectiu actual del projecte és establir una estructura sòlida i modular que faciliti el creixement del sistema. Això inclou preparar una base neta, organitzar correctament els components principals i definir una línia de desenvolupament que permeti ampliar el sistema amb noves funcionalitats en fases futures.

## Membres del grup

- Pep Catalina
- Jan Vargas
- Guillem Torras

## Logo

![Logo de DualOS](assets/logo.png)

> Pendent d’afegir la versió definitiva del logo.

## Captures, demo o fragments del projecte

Aquesta secció està pensada per mostrar visualment l’aspecte i el funcionament de DualOS. Aquí es poden afegir:



Exemple de futura captura:

```markdown
![Demo de DualOS](assets/demo.png)

````
## Instal·lació
Clona el repositori:
````
git clone https://github.com/usuari/DualOS.git
````
Entra a la carpeta del projecte:
````
cd DualOS
````
Obre la solució amb Visual Studio.<br>
Assegura’t que tens COSMOS correctament instal·lat i configurat.<br>
Compila el projecte des de Visual Studio.

## Execució

Un cop compilat, el sistema es pot executar des de l’entorn de COSMOS, habitualment en màquina virtual o en l’entorn de prova configurat pel projecte (VMware).
Actualment, DualOS utilitza una interfície gràfica bàsica implementada amb Cosmos Graphic Subsystem.


## Estructura del projecte

Una estructura bàsica del projecte pot ser la següent:
````text
DualOS/
│
├── assets/
│   └── logo.png
│
├── DualOS/
│   ├── Audios/
│   ├── Comands/
│   │   └── calculadora.cs
│   ├── CommandHistory.cs
│   ├── FileSystemManager.cs
│   ├── FtpManager.cs
│   ├── GraphicShell.cs
│   ├── GraphicsManager.cs
│   ├── Kernel.cs
│   ├── NetworkManager.cs
│   ├── Utilities.cs
│   ├── consola.cs
│   └── DualOS.csproj
│
├── README.md
├── LISENCE
└── DualOS.sln
````

## Arquitectura del sistema

El codi s'ha reorganitzat en diferents fitxers per millorar la seva llegibilitat i manteniment:

- `Kernel.cs` → Control principal del sistema i execució de comandes
- `FileSystemManager.cs` → Gestió del sistema de fitxers (VFS)
- `CommandHistory.cs` → Gestió de la memòria de comandes
- `Consola.cs` → Sistema d'ajuda (help)
- `Calculadora.cs` → Operacions matemàtiques
- `Utilities.cs` → Funcions auxiliars (logo, etc.)
- `GraphicsManager.cs` → Inicialització i gestió de la interfície gràfica amb Cosmos Graphic Subsystem
- `GraphicShell.cs` → Disseny i representació visual del shell gràfic
- `NetworkManager.cs` → Configuració de xarxa i assignació d’IP estàtica
- `FtpManager.cs` → Preparació inicial del servidor FTP

Aquesta separació permet una millor escalabilitat del sistema i facilita l'afegiment de noves funcionalitats.

## Funcionalitats implementades

### Sistema base
- Prompt interactiu
- guide, origin, clearvoid

### Sistema
- shutdown off / reboot

### Calculadora
- calc add, sub, mul, div, mod, sqrt

### Sistema de fitxers
- disks
- peek
- forge
- wipe
- write
- read

### Navegació
- jump amb suport de rutes relatives i absolutes

### Historial de comandes
S'ha implementat un sistema de memòria de comandes que emmagatzema les últimes 5 comandes executades.

Comandaments:
- history → mostra les últimes comandes
- !n → re-executa una comanda (ex: !0)

## Configuració del teclat
Per defecte, COSMOS OS utilitza el teclat americà. Per poder fer servir un teclat diferent (com l'espanyol), s'ha afegit la següent línia dins de la funció BeforeRun():
````
Sys.KeyboardManager.SetKeyLayout(new
Sys.ScanMaps.ESStandardLayout());
````

Això permet configurar el layout del teclat segons les necessitats de l'usuari.

### Interfície gràfica

S'ha iniciat la transició cap a Cosmos Graphic Subsystem (CGS).

Funcionalitats implementades:
- pantalla de benvinguda gràfica
- representació visual del logo i del nom del sistema
- shell gràfic bàsic
- panell superior amb informació del sistema
- panell lateral amb comandes principals
- zona central per mostrar la sortida de les comandes
- zona inferior per escriure comandes

### Xarxa i serveis

S'ha començat la implementació de funcionalitats de xarxa.

Funcionalitats implementades:
- configuració d’una IP estàtica
- comanda per consultar la IP configurada
- preparació inicial del servei FTP

Comandaments:
- `netconfig <ip> <mask> <gateway>` → configura una IP estàtica
- `ip` → mostra la IP configurada
- `ftpstart` → inicia o prepara el servei FTP en fase inicial
- `ftpstop` → apaga el servei FTP
- `ftpstatus` → indica l'estat del servei FTP


## Llicència

Aquest projecte està sota la llicència MIT. Consulta el fitxer LICENSE per a més informació.

## Estat del projecte

Actualment, DualOS es troba en una fase inicial-intermèdia de desenvolupament. Ja disposa d’una base funcional sobre COSMOS, un sistema de comandes modular, sistema de fitxers bàsic, historial de comandes, interfície gràfica inicial i una primera implementació de configuració de xarxa.

El servei FTP es troba en fase inicial i actualment està orientat a permetre la connexió bàsica, deixant la funcionalitat completa per a fases posteriors.
<br>

## Roadmap o millores futures

Les millores previstes per a DualOS inclouen:

- definir una pantalla d’inici més completa
- afegir menús interactius
- crear un sistema de comandes propi més avançat
- millorar l’organització modular del codi
- millorar la interfície gràfica basada en Cosmos Graphic Subsystem
- afegir més elements visuals al shell gràfic (panells, colors, disseny)
- completar la funcionalitat del servidor FTP
- afegir gestió d’usuaris i permisos per al servei FTP
- millorar la configuració de xarxa
- afegir persistència per a la configuració IP
- implementar comandes de monitoratge del sistema (CPU, memòria, uptime)
- ampliar la documentació tècnica del projecte
- afegir suport per funcionalitats addicionals del sistema operatiu





