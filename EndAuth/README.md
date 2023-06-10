# EndAuth REST API Endpoints

This repository contains a REST API built with .NET 6 that provides various endpoints for managing roles, identity, and users. Below is a list of available endpoints and their respective functionalities:

## Role Endpoints
### Get all roles

    GET /api/roles
    Retrieve all roles from the system.

### Get a specific role

    GET /api/roles/{roleId}
    Retrieve a specific role by its ID.

### Create a new role

    POST /api/roles
    Create a new role in the system.

### Update a role

    PUT /api/roles
    Update a specific role.

### Delete a role

    DELETE /api/roles/{roleId}
    Delete a specific role by its ID.

## Identity Endpoints

### User registration

    POST /api/identity/register
    Register a new user in the system.

### User login

    POST /api/identity/login
    Authenticate a user and obtain an access token.

### Refresh access tokens

    POST /api/identity/refreshTokens
    Refresh the access tokens for a user.

### Forgot password

    POST /api/identity/forgotPassword
    Initiate the password reset process.

### Reset password

    POST /api/identity/resetPassword
    Reset the password for a user.

## User Endpoints

### Get all users

    GET /api/users
    Retrieve all users from the system.

### Get a specific user

    GET /api/users/{userId}
    Retrieve a specific user by their ID.

### Update user information

    PUT /api/users
    Update user information for a specific user.

### Delete a user

    DELETE /api/users/{userId}
    Delete a specific user by their ID.

## Usage

To use the API endpoints, you need to send HTTP requests to the corresponding URLs using appropriate request methods (e.g., GET, POST, PUT, DELETE). Make sure to include any required parameters or request bodies as specified in the endpoint documentation.

For authentication and authorization, some endpoints may require an access token obtained through the login process. Include the access token in the request headers using the "Authorization" header field with the value "Bearer access_token".