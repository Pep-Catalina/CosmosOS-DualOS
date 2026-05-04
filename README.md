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
````
DualOS/
│
├── assets/
│   └── logo.png
│
├── Kernel.cs
├── Program.cs
├── README.md
└── DualOS.sln
├── GraphicsManager.cs
├── FileSystemManager.cs
├── CommandHistory.cs
├── Utilities.cs
├── consola.cs
└── Comands/
    └── calculadora.cs
````

## Arquitectura del sistema

El codi s'ha reorganitzat en diferents fitxers per millorar la seva llegibilitat i manteniment:

- `Kernel.cs` → Control principal del sistema i execució de comandes
- `FileSystemManager.cs` → Gestió del sistema de fitxers (VFS)
- `CommandHistory.cs` → Gestió de la memòria de comandes
- `Consola.cs` → Sistema d'ajuda (help)
- `Calculadora.cs` → Operacions matemàtiques
- `Utilities.cs` → Funcions auxiliars (logo, etc.)
- `GraphicsManager.cs` → Gestió de la interfície gràfica amb Cosmos Graphic Subsystem

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

S'ha iniciat la transició del sistema cap a Cosmos Graphic Subsystem (CGS).

Funcionalitats implementades:
- pantalla de benvinguda gràfica
- logo dibuixat amb formes i colors
- shell gràfic bàsic
- panell superior amb informació del sistema
- panell lateral amb comandes principals
- zona central per mostrar la sortida de les comandes
- zona inferior per escriure les comandes

## Llicència

Aquest projecte està sota la llicència MIT. Consulta el fitxer LICENSE per a més informació.

## Estat del projecte

Actualment, DualOS ja disposa d’una base funcional amb sistema de comandes, sistema de fitxers, historial de comandes i una primera interfície gràfica basada en Cosmos Graphic Subsystem.
<br>

## Roadmap o millores futures

Les millores previstes per a DualOS inclouen:

  - definir una pantalla d’inici més completa
  - afegir menús interactius
  - crear un sistema de comandes propi
  - millorar l’organització modular del codi
  - afegir configuració d’usuari i opcions del sistema
  - ampliar la documentació tècnica del projecte
  - afegir noves funcionalitats pròpies d’un sistema operatiu acadèmic
  - millorar la interfície gràfica basada en Cosmos Graphic Subsystem
  - afegir més elements visuals al shell gràfic
  - adaptar més funcionalitats internes al mode gràfic





