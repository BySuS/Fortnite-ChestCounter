<div align="center">

[English](README.md) · **Français** · [Español](README.es.md)

# Compteur de coffres Fortnite

[![version](https://img.shields.io/github/v/release/LED-N/Fortnite-ChestCounter?style=flat-square&label=version&color=e8b04b)](../../releases/latest) [![téléchargements](https://img.shields.io/github/downloads/LED-N/Fortnite-ChestCounter/total?style=flat-square&label=t%C3%A9l%C3%A9chargements&color=e8b04b)](../../releases) ![Windows 10 | 11](https://img.shields.io/badge/Windows-10%20%7C%2011-3a7bd5?style=flat-square) [![licence](https://img.shields.io/github/license/LED-N/Fortnite-ChestCounter?style=flat-square&label=licence&color=555)](LICENSE)

**Compte chaque coffre que tu ouvres dans Fortnite, partie après partie, avec un overlay en jeu.**

<img src="docs/overlay-fr.png" alt="Overlay entre deux parties" width="300">&nbsp;&nbsp;<img src="docs/overlay-match-fr.png" alt="Overlay pendant une partie" width="300">

[**⬇ Télécharger ChestCounter.exe**](../../releases/latest) · Windows 10 / 11 · gratuit · sans installation

</div>

---

Chest Counter lit les replays que Fortnite enregistre après chaque partie et tient le compte des coffres que tu as ouverts, ainsi que des **trésors de la chasse au trésor** obtenus grâce à tes coffres. Un overlay toujours au premier plan affiche tes totaux pendant que tu joues, et un rapport détaillé garde tout ton historique.

> Il compte les coffres **que tu as ouverts**. Il ne montre pas où se trouvent les coffres sur la carte.

## Comment ça marche (et pourquoi c'est sans risque)

Fortnite peut enregistrer un **fichier replay** de chaque partie sur ton PC. Chest Counter lit ces fichiers une fois la partie terminée, et rien d'autre.

- ✅ **Lit uniquement les replays** : les fichiers `.replay` que Fortnite écrit dans `%LOCALAPPDATA%\FortniteGame\Saved\Demos`. Ils sont ouverts en lecture seule et jamais modifiés.
- ✅ **Aucune modification du jeu** : rien n'est touché dans le dossier d'installation de Fortnite, aucun fichier n'est ajouté ni modifié.
- ✅ **Aucun accès à la mémoire du jeu** : l'outil ne lit, n'écrit et n'injecte jamais rien dans le processus de Fortnite. Il n'interagit pas du tout avec le jeu en cours.
- ✅ **Aucune donnée envoyée** : pas de connexion, pas d'accès à ton compte Epic, pas de télémétrie. Le seul accès réseau est la vérification d'une nouvelle version sur GitHub, désactivable dans les réglages.
- ✅ **Léger** : tourne en priorité basse (et en « mode efficacité » sous Windows 11), dort entre les parties, et l'overlay n'est redessiné que quand un chiffre change.

> [!IMPORTANT]
> **Les replays doivent être activés dans Fortnite** : *Paramètres → Jeu → Replays → Enregistrer les replays : **Activé***. Sans replays, l'outil n'a rien à lire.

Comme Fortnite n'écrit la clé de déchiffrement du replay qu'à la fin de la partie, les coffres sont comptés **quelques secondes après chaque partie**, pas pendant. Pendant que tu joues, l'overlay affiche **● PARTIE EN COURS**.

## Pour commencer

1. Télécharge **[`ChestCounter.exe`](../../releases/latest)**. Ce seul fichier suffit.
2. Active les replays dans Fortnite (voir ci-dessus).
3. Lance `ChestCounter.exe`.
   > Windows peut afficher *« Windows a protégé votre ordinateur »*, car l'exe n'est pas signé. Clique sur *Informations complémentaires → Exécuter quand même*.
4. Une courte installation (flèches + Entrée) te demande ta **langue** (français / anglais / espagnol), vérifie ton **dossier de replays**, propose de compter les **replays déjà présents** sur ton PC, et propose d'**ouvrir le compteur automatiquement au lancement de Fortnite**.

<p align="center"><img src="docs/setup-fr.png" alt="Installation au premier lancement" width="760"></p>

Si tu comptes tes replays existants, une barre de progression avance dans la fenêtre pendant que le rapport s'ouvre dans ton navigateur et se remplit en direct :

<p align="center"><img src="docs/progress-fr.png" alt="Barre de progression de l'analyse" width="760"></p>

5. Laisse la fenêtre ouverte (tu peux la réduire) pendant que tu joues. Quelques secondes après chaque partie, ses coffres sont ajoutés :

<p align="center"><img src="docs/console-fr.png" alt="Fenêtre principale" width="820"></p>

> [!TIP]
> Fortnite ne garde qu'un nombre limité de replays non sauvegardés et efface les plus anciens, et une partie jouée pendant que le compteur est fermé n'est pas comptée. Active **Ouvrir avec Fortnite** (proposé à l'installation, ou dans les réglages avec **C**) pour ne plus y penser.

## Ouvrir avec Fortnite

Avec cette option, un tout petit processus invisible démarre avec Windows et vérifie toutes les quelques secondes si Fortnite est lancé. Il ne lit rien du jeu : il regarde seulement la liste des programmes ouverts. Quand Fortnite démarre, il ouvre le compteur (réduit, le jeu garde l'écran). Quand Fortnite se ferme, le compteur compte ta dernière partie puis se ferme 30 secondes plus tard ; appuie sur une touche dans sa fenêtre pour le garder ouvert. Désactivable à tout moment dans les réglages (**C**) : le processus invisible s'arrête et est retiré du démarrage de Windows.

## L'overlay

- **En grand** : ton **total** de coffres (en or) et les coffres de ta **dernière partie**.
- **Session** : les coffres depuis le lancement de l'outil, avec le nombre de parties et la moyenne.
- **Dernière** : les coffres de ta dernière partie. **Trésors** : les trésors trouvés.
- Pendant une partie, le badge **● PARTIE EN COURS** apparaît et le compteur de droite affiche « … » jusqu'à la fin.

Pour le voir par-dessus le jeu, mets Fortnite en **« Plein écran fenêtré »** (*Paramètres → Vidéo → Mode de fenêtre*) : en plein écran exclusif, Windows ne peut rien afficher par-dessus le jeu. Trop grand, trop petit ou trop voyant ? Sa **taille** (70 à 175 %) et son **opacité** (100 à 30 %) se règlent avec **C**, et le changement est visible tout de suite. Tu streames ? `overlay.html` est le même overlay à fond transparent, pour une source navigateur **OBS**.

## Raccourcis

| Raccourci | Ce qu'il fait |
|---|---|
| **Ctrl+Maj+O** | Afficher / masquer l'overlay. Fonctionne partout, même en jeu. |
| **Ctrl+Maj+L** | Verrouiller / déverrouiller l'overlay. Fonctionne partout. **Verrouillé** : la souris passe au travers et il ne bouge plus (idéal en jeu). **Déverrouillé** : clic gauche maintenu pour le déplacer. |
| Clic droit sur l'overlay | Le masquer. |
| Cadenas sur l'overlay | Le verrouiller. |
| **R** | Ouvrir le rapport. |
| **O** / **L** | Comme Ctrl+Maj+O / Ctrl+Maj+L. |
| **P** | Changer de profil (plusieurs comptes sur ce PC). |
| **C** | Réglages : langue, ouvrir avec Fortnite, mises à jour, overlay au démarrage, taille et opacité de l'overlay, rapport au démarrage, dossier des replays. |
| **U** | Chercher une nouvelle version, et l'installer s'il y en a une. |
| **S** / **H** / **Q** | Résumé / aide / quitter. |

Les touches d'une lettre fonctionnent dans la fenêtre de l'outil. Si un autre programme utilise déjà Ctrl+Maj+O ou Ctrl+Maj+L, l'outil le signale, et les touches O et L restent utilisables.

## Le rapport

`report.html` s'ouvre dans ton navigateur et se met à jour tout seul : total, moyenne, record, coffres par jour, détail par type de coffre, trésors et chaque partie.

<p align="center"><img src="docs/report-fr.png" alt="Rapport" width="820"></p>

## Trésors

- **★★ Coffre doré (certain).** Quand un coffre rapporte une récompense de la chasse au trésor (cosmétique, piste musicale, écran de chargement, véhicule…), le serveur fait apparaître un coffre doré qui porte l'**identifiant de la récompense**. L'outil le lit dans le replay.
- **★ Chaque trésor reçu (moment exact).** Chaque trésor reçu est enregistré avec son moment exact et le coffre d'où il vient ; il n'est compté que si ce coffre est **le tien** (les trésors partagés par un coéquipier ne comptent pas). Quand l'objet est identifiable, il est nommé ; sinon il s'affiche **« Gizmo de trésor »** (feu d'artifice doré, extracteur portable, localisateurs, accélérateur d'extraction), dont le type exact n'est pas dans le replay.
- Un trésor venu d'autre chose qu'un coffre (canon à butin, caisse de munitions…) est signalé par ⚑ dans le rapport.

Le jeu ne transmet que des identifiants, pas les noms : `treasures.json` fait la correspondance et se modifie avec le Bloc-notes. Les nouveaux identifiants y sont ajoutés automatiquement, et l'outil te prévient quand l'un d'eux a besoin d'un nom.

## Profils (plusieurs comptes sur un PC)

Les replays enregistrés sur un PC sont normalement ceux de son joueur : **pas de pseudo à taper**, l'outil compte le joueur qui a enregistré chaque replay. S'il trouve des replays de plusieurs comptes, chaque compte a son profil avec ses propres statistiques. L'outil demande alors à chaque lancement sur quel compte tu joues, et tu peux changer à tout moment avec **P**. Une partie jouée sur un autre compte est enregistrée dans le profil de ce compte, jamais perdue.

## Mises à jour

Au démarrage, le compteur vérifie s'il existe une nouvelle version sur GitHub. Si c'est le cas, il te prévient, et la touche **U** la télécharge, la vérifie et redémarre le compteur : ton historique et tes réglages sont conservés. Le compteur était déjà ouvert quand la version est sortie ? Appuie sur **U** : il revérifie sur GitHub. Dans les réglages (**C**), tu peux choisir *proposées* (par défaut), *installées automatiquement* (seulement entre deux parties) ou *désactivées* (aucun accès réseau).

## Tes données

Tout est rangé dans `%APPDATA%\ChestCounter` :

| Fichier | Contenu |
|---|---|
| `report.html` | Le rapport (s'ouvre dans ton navigateur). |
| `overlay.html` | Overlay transparent pour OBS. |
| `matches.csv` | Une ligne par partie, pour Excel ou Google Sheets. |
| `data.json` | Tout ton historique. **Sauvegarde-le** si tu changes de PC. |
| `config.json` | Langue, profil, dossier des replays, position, taille et opacité de l'overlay. |
| `treasures.json` | Les noms des récompenses de la chasse au trésor. |
| `errors.log` | Le détail des replays illisibles, à joindre à un rapport de bug. |

Pour tout recommencer : lance `ChestCounter.exe --reset-config`, ou supprime ce dossier. Si tu utilisais l'ancienne version (ChestTracker), ton dossier `%APPDATA%\ChestTracker` est repris automatiquement.

## FAQ

**Rien n'est compté.** Vérifie que les replays sont activés dans Fortnite, et que l'outil tourne quand la partie se termine. La fenêtre te prévient si le dossier des replays est absent ou vide.

**Les caisses de munitions sont-elles comptées ?** Non. Caisses de munitions, de nourriture et canons à butin sont listés à part dans le rapport et n'entrent pas dans le total. Les coffres que tu **détruis** au lieu de les ouvrir sont comptés. Un coffre qui se remplit à nouveau pendant la partie et que tu rouvres compte une fois de plus ; repasser près d'un coffre déjà ouvert (coffre enterré creusé, coffre de reliques) ne le compte pas deux fois.

**Et les modes créatifs ?** Seules les parties Battle Royale sont comptées ; les autres modes sont ignorés.

**Fortnite s'est fermé en pleine partie.** Son replay est incomplet (Fortnite n'a jamais écrit la clé de déchiffrement) : cette partie ne peut pas être comptée, et l'outil la signale comme « replay incomplet ».

**« Impossible de lire ce replay ».** Une mise à jour de Fortnite peut changer le format des replays ; le parseur doit alors être mis à jour. Les parties déjà comptées sont conservées. [Ouvre un ticket](../../issues) en joignant `errors.log` (dans `%APPDATA%\ChestCounter`).

**Déjà ouvert ?** Un seul compteur peut tourner à la fois : le relancer indique simplement qu'il est déjà ouvert.

**Une partie peut-elle être comptée deux fois ?** Non, même si tu renommes ou sauvegardes le replay.

## Options en ligne de commande

```
ChestCounter.exe --lang <fr|en|es>   change la langue
ChestCounter.exe --demos <dossier>   autre dossier de replays
ChestCounter.exe --data <dossier>    autre dossier de données
ChestCounter.exe --once              analyse les replays terminés puis quitte
ChestCounter.exe --reset-config      relance l'installation
ChestCounter.exe --overlay-preview <fichier.png>   enregistre des aperçus de l'overlay puis quitte
```

## Compiler soi-même

Installe le [SDK .NET 10](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows x64), puis double-clique sur **`build.bat`** : l'exe est écrit dans `dist\ChestCounter.exe`. Le workflow GitHub Actions compile l'exe à chaque push et le joint à une release quand un tag `v*` est poussé.

## Licence et crédits

[MIT](LICENSE). La lecture des replays repose sur [FortniteReplayDecompressor](https://github.com/Shiqan/FortniteReplayDecompressor) de Shiqan (MIT), légèrement modifié : voir [`lib/PATCHES.md`](lib/PATCHES.md).

Ce projet n'est ni affilié, ni approuvé, ni sponsorisé par Epic Games. Fortnite est une marque d'Epic Games, Inc.
