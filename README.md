# Command Line Application for our Blaise NuGet Package

This is a CLI console application that wraps around our [Blaise NuGet package](https://github.com/ONSdigital/blaise-nuget-api).

This is used as part of data delivery and for configuring Blaise.

# Usage

Set the Blaise connection details and the database string in the `app.config` file within the `Blaise.Cli` project.

Help can be accessed with the following comamnd:

```
blaise.cli --help
```

# Creating data interface files

https://help.blaise.com/Blaise.html?blaise_data_interface.htm

The data interface files tell Blaise how to connect to the database targets. They also create the objects required in the database, essentially the tables and columns.

Use the verb 'datainterface' to access the command, it then requires two parameters: 

- **type**, long name `--type`, short name `-t`, the type of data interface file you wish to create (Cati, Cari, AuditTrail, Configuration, Meta, Session, Credentials)
- **file**, long name `--file`, short name `-f`, the name of the file you wish to create

**Examples**

Crate data interface file using long names:

```
blaise.cli datainterface --type cati --file catidb
```

Crate data interface file using short names:

```
blaise.cli datainterface -t cati -f catidb
```
