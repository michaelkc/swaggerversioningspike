﻿These test lock down the backwards compatible behaviour of the version being tested. 
They can (and should) be retired when the version being tested moves from obsolete to removed.

This project must never reference anything but Startup in the Host project (required for WebApplicationFactory testing) directly, as to prevent automatic refactorings from changing code.
The tests must run against the service endpoints with their own service proxies generated manually in this project or manually via Swagger.



# Running the same tests against all endpoint versions, opting in/out on specific versions for specific tests
## Approaches

- let the TestFixture iterate for all versions by default (e.g. TestFixtureSource), letting individual Tests override (not supported out of the box)
- let the individual Tests iterate for all versions by default (e.g. TestCaseSource), using metadata provided to override 

## Using TestFixtureSource / fixture-level constructs
TestFixtureSource is unusable "as is", as it cannot accept additional metadata (it will always run the entire fixture(s) for any versions generated by it). I tried implementing opt-out support by

- deriving TestCaseAttribute with additional opt-out metadata 
  - (nothing is virtual so cannot poke behaviour)
  - Could add metadata via marker attribute (e.g. "[VersionsSupportedByTest("V1","V2")]" but that requires a hook in TestFixtureSource to ignore tests based on attribute metadata)
- deriving TestCaseAttributes interfaces with additional opt-out metadata 
  - might work, but high risk of issues with the various test runners
- deriving TestFixtureSourceAttribute 
  - (nothing is virtual so cannot poke behaviour)
- deriving IFixtureBuilder2 / IFixtureBuilder directly as an alternative to deriving TestFixtureSourceAttribute 
  - (tests were not recognized- https://youtrack.jetbrains.com/issue/RSRP-476083)


## Using TestCaseSource
- all input to attribute must be compile-time constant, which is the cause of the clunky interface to TestCaseSource
- decorating TestCaseSource by implementing its interfaces is possible, but subject to same constraints. Makes it difficult to split the TestCaseSource implementation allowing metadata-based
  opt-in/out from the project-specific definition of versions, without ending up with a similarly clunky interface as TestCaseSource
- in a pool of bad options, using TestCaseSource directly with a per-project, conventionally created TestCaseSource SourceType ("ApiVersions") looks like the better option
 - out-of-the box, so no problems with test runners
 - well documented in the NUnit documentation
 - <~20 SLOC
 - attribute usage, while clunky, is explicit, so it is easy to see which versions are being tested
