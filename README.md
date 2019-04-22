# Demo eShopOnWeb

This is a custom implementation of eShopOnWeb application (https://github.com/dotnet-architecture/eShopOnWeb).

The differences between this one and the original are:
- Using MSTest instead of XUnit.
- Reorganized tests.
- Added a small deployment project.

The purpose of this custom implementation is to be an example about how to integrate Azure DevOps in order to deploy this project with a Build and Release pipeline. For this, the projects provide a different set of tests:
- Unit tests.
- Integration tests.
- Functional tests.
- UI test with Selenium.
- SonarCloud integration during build phase.

In addition, the appliaction is forced to use an InMemory database just to simplify the deployment infrastructure despite of the original example where it uses InMemory in development environment and an actual one in production.

## Prerrequisites

### Visual Studio 
Even the project is prepared to clone it directly from GibHub to your Azure DevOps, you can clone it on your local repository on your machine and build it. Visual Studio 2017 is required on that -Community version is the one used to create this project.

### .NETCore SDK
In order to build the project locally it is required to have installed .NET Core 2.2 version. Use this link to download it on your machine (https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.106-windows-x64-installer
https://blog.testproject.io/2018/12/18/net-core-test-automation-selenium-page-object-page-factory/) with Visual Studio 2017.

### Microsoft Azure
The deployment pipeline works with Azure to deploy the website and it creates all the resources required. You have to be registered with an Azure Subscription and create a resource group where the deployment can be performed.

## Azure DevOps configuration
An Azure DevOps account it is required to perform all the activities with the pipelines:
- Git repository
- Build pipeline
- Release pipeline
- Service connection

### Service connection: SonarCloud
A SonarCloud account should be created to cover some steps in the Build pipeline. The idea is to show how we can use a tool to perform statci analysis into the code. To provide connectivity with your DevOps account and Sonar, please follow next steps:
1. Go to SonarCloud and create an account. You can use either your work/school account, or if you don't have, an account created by your won Azure Active Directory will work. As the account is free, the project should be public.
2. Install SonarCloud extensions tasks from Azure DevOps marketplace. This will provide a set of tasks related with SonarCloud in the Build pipeline.

More information about SonarCloud here https://sonarcloud.io/documentation/integrations/vsts/

### Service connection: Azure Resource Manager.
As the deployment will be performed in Azure, it is required to setup the connetiivty between your Azure account and your Azure DevOps account. You should have an account in Azure to create the service connection.
- It is desirable to create the account with an existing service principal, just to have a way to track which applications can access to your Azure subscription.

Further details in https://docs.microsoft.com/en-us/azure/devops/pipelines/library/connect-to-azure?view=azure-devops

### Build & Release definitions
Both definitions are provided in JSON in the "azure_pipelines" folder. You can use them to create your pipeline and update it with the right settings. Remember to set up the right host machine (it should be Hosted VS2017) and use the right path to your project and repository during the import process.
- Build: Provide the name of the SonarCloud service endpoint previously created.
- Release: Provide the name of the Azure Resource Manager service endpoint previously created. Link the "Source" with the build created in previous step and ensure the name of the artifact is "Source".
