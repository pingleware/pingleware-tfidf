# pingleware-tfidf

Node package for TFIDF which stands for Transform Frequency Inverse Document Frequency which given a textual prose will return the three topic sentences

`npm test`

will produce the results of

```
[
  'There was nobody there, but he heard the voice again',
  'He thought he heard a voice but at this moment all he could hear was the wind rustling the leaves of the trees all around him',
  'The choice was red, green, or blue'
]
```

then using ChatGPT to create a new story from these topic sentences,

```
create a new story from the following three topic sentences 'There was nobody there, but he heard the voice again',
  'He thought he heard a voice but at this moment all he could hear was the wind rustling the leaves of the trees all around him',
  'The choice was red, green, or blue'
```

will result in,

```
Title: The Whispering Woods

In the heart of the dense forest, where sunlight struggled to penetrate the thick canopy of ancient trees, a lone figure stood, surrounded by the haunting silence of the wilderness. His name was Ethan, a young explorer with a passion for uncovering the secrets of nature's most enigmatic realms.

As Ethan trekked deeper into the woods, an eerie sensation crept over him. He thought he heard a voice, faint and distant, echoing through the rustling leaves. Pausing in his tracks, he strained his ears, but all he could discern was the gentle whisper of the wind among the branches.

Undeterred, Ethan pressed on, his curiosity driving him forward. There was nobody there, yet he heard the voice again, clearer this time, as if beckoning him deeper into the heart of the forest.

Guided by the mysterious voice, Ethan stumbled upon a clearing, bathed in an otherworldly glow. At its center stood three pedestals, each adorned with a single object—a shimmering gem of vibrant hue.

Approaching cautiously, Ethan realized the significance of the choice before him. The voice urged him to decide: red, green, or blue. Each gem seemed to pulsate with its own unique energy, offering a path into the unknown.

With a mixture of trepidation and excitement, Ethan reached out, his hand hovering over the glowing gems. A decision weighed heavily upon him, for he knew that his choice would shape the course of his journey.

After a moment's hesitation, Ethan's fingers closed around the cool surface of the green gem. As he held it aloft, a surge of power coursed through him, filling him with newfound resolve.

With the echo of the voice fading into the depths of the forest, Ethan embarked on a quest unlike any other, guided by the whispers of the woods and the boundless mysteries that awaited him.
```

## .Net Console Version

The .Net console version is a standalone executable built using .Net

```
dotnet new console --framework net8.0 --use-program-main
```

To build an executable, use

```
dotnet publish
```

the options are

```
Description:
  Publisher for the .NET Platform

Usage:
  dotnet publish [<PROJECT | SOLUTION>...] [options]

Arguments:
  <PROJECT | SOLUTION>  The project or solution file to operate on. If a file is not specified, the command will search the current directory for one.

Options:
  --ucr, --use-current-runtime         Use current runtime as the target runtime.
  -o, --output <OUTPUT_DIR>            The output directory to place the published artifacts in.
  --artifacts-path <ARTIFACTS_DIR>     The artifacts path. All output from the project, including build, publish, and pack output, will go in 
                                       subfolders under the specified path.
  --manifest <MANIFEST>                The path to a target manifest file that contains the list of packages to be excluded from the publish step.
  --no-build                           Do not build the project before publishing. Implies --no-restore.
  --sc, --self-contained               Publish the .NET runtime with your application so the runtime doesn't need to be installed on the target 
                                       machine.
                                       The default is 'false.' However, when targeting .NET 7 or lower, the default is 'true' if a runtime identifier 
                                       is specified.
  --no-self-contained                  Publish your application as a framework dependent application. A compatible .NET runtime must be installed on 
                                       the target machine to run your application.
  --nologo                             Do not display the startup banner or the copyright message.
  -f, --framework <FRAMEWORK>          The target framework to publish for. The target framework has to be specified in the project file.
  -r, --runtime <RUNTIME_IDENTIFIER>   The target runtime to publish for. This is used when creating a self-contained deployment.
                                       The default is to publish a framework-dependent application.
  -c, --configuration <CONFIGURATION>  The configuration to publish for. The default is 'Release' for NET 8.0 projects and above, but 'Debug' for 
                                       older projects.
  --version-suffix <VERSION_SUFFIX>    Set the value of the $(VersionSuffix) property to use when building the project.
  --interactive                        Allows the command to stop and wait for user input or action (for example to complete authentication).
  --no-restore                         Do not restore the project before building.
  -v, --verbosity <LEVEL>              Set the MSBuild verbosity level. Allowed values are q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic].
  -a, --arch <ARCH>                    The target architecture.
  --os <OS>                            The target operating system.
  --disable-build-servers              Force the command to ignore any persistent build servers.
  -?, -h, --help                       Show command line help.
```

for the current build system

```
dotnet publish --output "dist" --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
```

to specify the runtime, add --runtime with the configuration

| OS                               | RUNTIME            | COMMAND                                                                                                                                                                           |
| -------------------------------- | ------------------ | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Mac Apple Silicon                | osx-arm64          | `dotnet publish --output "dist/osx/arm64" --runtime osx-arm64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`                   |
| Mac Intel                        | `osx-x64`        | `dotnet publish --output "dist/osx/x64" --runtime osx-x64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`                       |
| Windows (x86)                    | win-x86            | `dotnet publish --output "dist/win/x86" --runtime win-x86 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`                      |
| Windows (x64)                    | win-x64            | `dotnet publish --output "dist/win/x64" --runtime win-x64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`                       |
| Windows (arm64)                  | win-arm64          | `dotnet publish --output "dist/win/arm64" --runtime win-arm64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`                   |
| Linux (x64)                      | linux-x64          | `dotnet publish --output "dist/linux/x64" --runtime linux-x64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`                   |
| Linux MUSL (x64)                 | linux-musl-x64     | `dotnet publish --output "dist`/linux/`musl/x64" --runtime linux-musl-x64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`     |
| Linux MUSL (arm64)               | linux-musl-arm64   | `dotnet publish --output "dist`/linux/`musl/arm64" --runtime linux-musl-arm64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true` |
| Linux (arm)<br />Raspberry Pi 2+ | linux-arm          | `dotnet publish --output "dist`/linux/`arm" --runtime linux-arm --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`               |
| Linux (arm64)                    | linux-arm64        | `dotnet publish --output "dist/linux/arm64" --runtime linux-arm64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true`               |
| Linux Bionic (arm64)             | linux-bionic-arm64 |                                                                                                                                                                                   |
| iOS                              | ios-arm64          |                                                                                                                                                                                   |
| Android                          | android-arm64      |                                                                                                                                                                                   |

## Installation

`npm i @pingleware/tf-idf`

## Usage

```
"use strict"

const TFIDF = require("@pingleware/tf-idf");

//
// Generated by https://randomwordgenerator.com/paragraph.php
//
const prose = `The irony of the situation hadn't escaped her. She had taken years to sculpt the perfect persona with the perfect look that she shared on Instagram. She knew her hundreds of thousands of followers envied that life she showed and stayed engaged with her because they wanted that life too. The truth was that she wanted the perfect life she portrayed more than any of her fans. The fact was that despite all the perfection she shared on social media, her life was actually more of a mess than most.
Was it a whisper or was it the wind? He wasn't quite sure. He thought he heard a voice but at this moment all he could hear was the wind rustling the leaves of the trees all around him. He stopped and listened more intently to see if he could hear the voice again. Nothing but the wind rustling the leaves could be heard. He was about to continue his walk when he felt a hand on his shoulder, and he quickly turned to see who it was. There was nobody there, but he heard the voice again.
The choice was red, green, or blue. It didn't seem like an important choice when he was making it, but it was a choice nonetheless. Had he known the consequences at that time, he would likely have considered the choice a bit longer. In the end, he didn't and ended up choosing blue.`;

const results = TFIDF(prose);

console.log(results);


```

will result in

```
[
  'There was nobody there, but he heard the voice again',
  'He thought he heard a voice but at this moment all he could hear was the wind rustling the leaves of the trees all around him',
  'The choice was red, green, or blue'
]
```

## Resources

* [https://en.wikipedia.org/wiki/Tf%E2%80%93idf](https://en.wikipedia.org/wiki/Tf%E2%80%93idf)
* [https://monkeylearn.com/blog/what-is-tf-idf/](https://monkeylearn.com/blog/what-is-tf-idf/)
* [https://www.capitalone.com/tech/machine-learning/understanding-tf-idf/](https://www.capitalone.com/tech/machine-learning/understanding-tf-idf/)
* [https://www.geeksforgeeks.org/understanding-tf-idf-term-frequency-inverse-document-frequency/](https://www.geeksforgeeks.org/understanding-tf-idf-term-frequency-inverse-document-frequency/)
* [https://www.learndatasci.com/glossary/tf-idf-term-frequency-inverse-document-frequency/](https://www.learndatasci.com/glossary/tf-idf-term-frequency-inverse-document-frequency/)
* [https://www.onely.com/blog/what-is-tf-idf/](https://www.onely.com/blog/what-is-tf-idf/)

## Release Schedule

|    Date    | Version | Description     |
| :--------: | :-----: | --------------- |
| 2024-02-23 |  1.0.0  | Initial release |
|            |        |                 |

## End-of-Life

When a piece of software is useful, there should never be an EOL doctrine. The intention for this application is to achieve immortality ;). At some point of time in the future, this project may appear to be dead and abandon. The opposite will be true! When this project reaches that stage, this project has matured to a level where maintenance is minimal (mostly updating to latest version of Node).

```
Patrick O. Ingle
February 23, 2024
```
