These test lock down the backwards compatible behaviour of the version being tested. 
They can (and should) be retired when the version being tested moves from obsolete to removed.

This project must never reference anything but Startup in the Host project (required for WebApplicationFactory testing) directly, as to prevent automatic refactorings from changing code.
The tests must run against the service endpoints with their own service proxies generated manually in this project or manually via Swagger.