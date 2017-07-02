# PPWCode.Util.Log4Net

This library is part of the PPWCode project.

## What

The library currently contains an S3 appender for Log4Net. This is an appender that
will write out the logs to files on an S3 bucket.

## How

Usage is really simple: first add the correct version of the package to your project
(this depends on the used log4net version), next configure the appender.

## Configuration

An example configuration is given here:

    <appender name="S3Appender" type="PPWCode.Util.Log4Net.I.S3Appender">
        <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="%date [%-4thread] %-5level %logger [%method:%line] - %message%newline" />
        </layout>
        <bufferSize>64</bufferSize> <!-- size of buffer in log entries -->
        <region>eu-west-1</region> <!-- region for S3 bucket -->
        <bucketName>my-software.somewhere.net</bucketName> <!-- name of S3 bucket -->
        <keyPrefix>logs/my-software</keyPrefix> <!-- prefix for the name of the logs -->
    </appender>


AWS authentication: the process that writes to the logs, must obviously have write rights on
the S3 bucket.  For the process to authenticate itself, there are several solutions:

1. Amazon EC2 instance has rights to the S3 bucket.
2. Using AWS credentials through profiles.
3. Specifying the access and secret keys.


### Amazon EC2 instance

Nothing extra needs to be configured. Access will be granted automatically.


### Using profiles

The profile needs to be specified in the configuration file.

This can be done as follows:

    <appSettings>
        <add key="AWSProfileName" value="development"/>
    </appSettings>


### Specifying the access and secret keys

The access and secret keys can be specified in the configuration file as follows:

    <appSettings>
        <add key="AWSAccessKey" value="xxxxxxxxxxxx"/>
        <add key="AWSSecretKey" value="xxxxxxxxxxxxxxx"/>
    </appSettings>

Note that these keys can also be passed in environment variables: `AWS_ACCESS_KEY_ID`
and `AWS_SECRET_ACCESS_KEY`.


## Getting started

### PPWCode.Util.Log4Net I

This is version I of the library, which is designed to work with Microsoft .NET 4.5.

The library is available in the form of several [NuGet] packages `PPWCode.Util.Log4Net.I.*`
in the [NuGet Gallery], each one built against another version of Log4Net.  The packages
can be installed using the NuGet package manager in Visual Studio.


## Build your own

A couple of reasons come to mind as to why you would want to build your own package of
this library. One reason would be that you need a version of the library built
with the debug configuration. Another reason might be that you need features
that are available on master, but that are not yet released.

Building your own package of this library is very easy.  A [psake] build script is
added for this purpose.

Before executing regular [psake] tasks, the environment must first be initialized.
To do this, open a PowerShell prompt, and execute the following in the root folder
of the source.

    .\init-psake.ps1

This will initialize your environment. Note that the script assumes that the
[NuGet] commandline client is available on the path.

After the initialization, several [psake] tasks can be executed using the
PowerShell command `Invoke-psake` that is available now. Here are a couple
of examples:

    Invoke-psake
    Invoke-psake ?
    Invoke-psake PackageRestore
    Invoke-psake Package -properties @{ 'buildconfig'='Debug'; 'repos'=@('nuget'); 'publishrepo' = 'local' }

The last line builds a [NuGet] package using the 'Debug' configuration, and publishes
it to the [NuGet] repository with the name 'local'. The [NuGet] repository 'nuget'
is used to locate the dependent [NuGet] packages.


## Contributors

See the [GitHub Contributors list].


## PPWCode

This package is part of the PPWCode project, developed by [PeopleWare n.v.].

More information can be found in the following locations:
* [PPWCode project website]
* [PPWCode Google Code website]

Please note that not all information on those sites is up-to-date. We are
currently in the process of moving the code away from the Google code
subversion repositories to git repositories on [GitHub].


### PPWCode .NET

Specifically for the .NET libraries: new development will be done on the
[PeopleWare GitHub repositories], and all new stable releases will also
be published as packages on the [NuGet Gallery].

The packages include both the pdb and xml files, for debugging symbols
and documentation respectively.  In the future we might look into using
symbol servers.


## License and Copyright

Copyright 2017 by [PeopleWare n.v.].

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.



[PPWCode project website]: http://www.ppwcode.org
[PPWCode Google Code website]: http://ppwcode.googlecode.com

[PeopleWare n.v.]: http://www.peopleware.be/

[NuGet]: https://www.nuget.org/
[NuGet Gallery]: https://www.nuget.org/policies/About

[GitHub]: https://github.com
[PeopleWare GitHub repositories]: https://github.com/peopleware

[Microsoft Code Contracts]: http://research.microsoft.com/en-us/projects/contracts/

[psake]: https://github.com/psake/psake

[GitHub Contributors list]: https://github.com/peopleware/net-ppwcode-util-test/graphs/contributors
