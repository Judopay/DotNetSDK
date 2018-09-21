# Deployments
There are multiple stages to making changes to the SDK. First is to pull down the repository locally and make sure everything builds and that the tests are successful

After you have made and pushed your changes, app veyor will build and test your changes.

Pull requests should be made against master. Once approved they can be merged. Once merged to master, app veyor will deploy the changed to judo's internal nuget feed. This is so we can pull the package into sample projects so that we can make sure nothing breaks.

Once the master branch has been tested sufficiently, a release within github will be made which will create a tag and deploy to nuget


