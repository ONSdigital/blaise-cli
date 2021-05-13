# Command Line Application around Blaise Nuget Package

Application is designed to be used as a console application as a wrapper around the Blaise Nuget Package.

#usage

Blaise connection data including the DB string is stored within the app.settings for Blaise.Cli

Access help via cmd:
```
blaise.cli --help
```
##Creating Data interface file

Use the verb 'datainterface' to access the command, it then requires two parameters: 
- **type**, long name *--type*, short name *-t* the type of data interface file you wish to create e.g (Cati, Cari, AuditTrail, Configuration, Meta, Session)
- **file**, long name *--file*, short name *-f* the name of the file you wish to create

###Example 

**Create Data interface**
*Long name*
```
blaise.cli datainterface --type cati --file catidb
```
*Short name*
```
blaise.cli datainterface -t cati -f catidb
```

