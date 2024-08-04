# Blaise CLI

CLI tool which uses our NuGet package.

Installed on VMs in our environments to aid Blaise configuration and data delivery.

## Usage

Set environment variables required in the `App.config` file, **NEVER** commit this file with populated values! If running locally it's safer to set system wide environment variables on your machine. Example:

```
setx ENV_BLAISE_SERVER_HOST_NAME=blah /m
```

Access help:

```
blaise.cli --help
```

## Creating data interface files

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

 ## Running data delivery pipelines

 Extracts a questionnaires data out of Blaise for delivery. Used as part of our data delivery pipeline.

Use the command `datadelivery` with the following parameters:

| Long | Short | Description |
| --- | --- | --- |
| serverParkName | s | Name of the server park that houses the questionnaire to be delivered |
| questionnaireName | q | Name of the questionnaire to be delivered |
| file | f | File name of the questionnaire package to be delivered |
| audit | a | Option to include audit trail data as part of the delivery |
| batchSize | b | The number of cases to use for batching |

 ## Installing questionnaires

 Installs a Blaise questionnaire package into a Blaise environment. Used to install CMA packages during Blaise configuration.

Use the command `questionnaireinstall` with the following parameters:

| Long | Short | Description |
| --- | --- | --- |
| questionnaireName | q | Name of the questionnaire to be installed |
| serverParkName | s | Name of the server park to install the questionnaire |
| questionnaireFile | f | File name of the questionnaire package to be installed |
