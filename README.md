# NGU Save to Json

Convert your NGU save into a JSON format to use in other apps such as [Gear Optimizer](https://gmiclotte.github.io/gear-optimizer/#/) or to create your own apps! This is a rough first iteration. Updates and improvements can be made based on feedback, so please leave feedback.

Recently https://ngusav.es went down due to the domain expiring, along with the sharing functionality. I created this as a way for people to still be able to get their save in a json format. Most of the logic here comes from jlweston's [NGU Save Analyser](https://github.com/jlweston/ngu-save-analyser), so a huge thanks to him.

## Usage

### Dependencies
Make sure you have .NET Core currently installed on the machine you plan to run this application on. If you have any problems with installation or unfamiliar with, I have provided resources below.

- [.NET Core](https://dotnet.microsoft.com/download)

### Running the app
- Download the [latest release](https://github.com/tiemonl/NGUSaveToJSON/releases/latest) for your OS.
- Once downloaded, unzip the file.
- Open a terminal and cd to the folder location.
- Once inside run this command (replacing <path/to/your/NGUSave-Build-file>):
    - `dotnet NGUSaveToJson.dll <path/to/your/NGUSave-Build-file>`
- This will read your save and output a file called `ngusave.json` in the same directory as your save.

### Demo

## Contributing

#### Bug Reports & Feature Requests

Please use the [issue tracker](https://github.com/tiemonl/NGUSaveToJSON/issues) to report any bugs or file feature requests.

#### Developing

PRs are welcome. To begin developing, do this:

```bash
git clone git@github.com:tiemonl/NGUSaveToJSON.git
cd NGUSaveToJSON/
```