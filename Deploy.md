# Updates to the SDK
There are multiple stages to making changes to the SDK. First is to pull down the repository locally and make sure everything builds and that the tests are successful

Update the SDK version in JudoPayDotNet properties, Package-General-Package Version. After you have made and pushed your changes, CircleCI will build and test your changes.

Pull requests should be made against master. Once approved they can be merged.

Once the master branch has been tested sufficiently, a release within github will be made which will create a tag (vx.y.z) and deploy to nuget


