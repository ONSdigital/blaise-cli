# Blaise CLI

A command-line tool for interacting with Blaise to aid in configuration, data delivery, and questionnaire management.

This tool is installed on VMs in our environments to automate and simplify common Blaise operations.

## Usage

To run the service locally, you must provide the necessary connection details for a Blaise environment. You can achieve this in two ways:

- **Populate `App.config`:** Update the `App.config` file with the required Blaise connection details.
- **Use Environment Variables:** Alternatively, you can use `setx` commands to set environment variables. This is a safer way to handle sensitive data. For example: `setx ENV_BLAISE_SERVER_HOST_NAME=blah /m`.

⚠️ **Important:** Never commit `App.config` files with populated secrets or credentials to source control. To safely commit your changes without including the `App.config` file, you can use the command: `git add . ':!app.config'`.

Once running you can access the help menu with the following command:

```
blaise.cli --help
```

### Creating data interface files

Blaise uses data interface files to setup its various databases. Used in the Blaise configuration pipeline.

Use the command `datainterface` with the following parameters:

| Long | Short | Description |
| --- | --- | --- |
| type | t | Type of data interface file to be created |
| file | f | File name of data interface file to be created |

Long parameter example:

```
blaise.cli datainterface --type cati --file catidb
```

Short parameter example:

```
blaise.cli datainterface -t cati -f catidb
```

### Running data delivery

Extracts a questionnaires data out of Blaise for delivery. Used as part of our data delivery pipeline.

Use the command `datadelivery` with the following parameters:

| Long | Short | Description |
| --- | --- | --- |
| serverParkName | s | Name of the server park that houses the questionnaire to be delivered |
| questionnaireName | q | Name of the questionnaire to be delivered |
| file | f | File name of the questionnaire package to be delivered |
| audit | a | Option to include audit trail data as part of the delivery |
| batchSize | b | The number of cases to use for batching |

Long parameter example:
```
blaise.cli datadelivery --serverParkName park1 --questionnaireName OP N2101a --file OPN2101a.zip --audit
```

Short parameter example:
```
blaise.cli datadelivery -s park1 -q OPN2101a -f OPN2101a.zip -a
```

### Installing a questionnaire package

Installs a Blaise questionnaire package into a Blaise environment. Used to install CMA packages during Blaise configuration.

Use the command `questionnaireinstall` with the following parameters:

| Long | Short | Description |
| --- | --- | --- |
| questionnaireName | q | Name of the questionnaire to be installed |
| serverParkName | s | Name of the server park to install the questionnaire |
| questionnaireFile | f | File name of the questionnaire package to be installed |

Long parameter example:
```
blaise.cli questionnaireinstall --questionnaireName OPN2101a --serverParkName park1 --questionnaireFile "C:\temp\OPN2101a.bpkg"
```

Short parameter example:
```
blaise.cli questionnaireinstall -q OPN2101a -s park1 -f "C:\temp\OPN2101a.bpkg"
```

## Coding Standards

The project enforces a strict set of coding and formatting rules via an `.editorconfig` file, which is used by StyleCop. Builds may error or issue warnings if these standards are not followed. You can use `dotnet format` to automatically fix some formatting issues.
