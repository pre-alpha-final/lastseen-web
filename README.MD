# LastSeen
Simplifies tracking the shows You watch.

# Screens
[![](/docs/screens.jpg)](https://lastseen.azurewebsites.net)

# Demo environment
For testing purposes log in here:  
**https://lastseen.azurewebsites.net**  
with these credentials:  
User: **demo@example.com**  
Password: **demo**  

# Development
Settings needed for development, mirrored in secrets.json
```
{  
  "ConnectionStrings":{
    "UsersDbContext":"",
    "LastSeenWebDb":""
  },
  "SendGridApiKey":"",
  "ClientSecret":"",
  "Authority":"",
  "Domain":"",
  "IdentityJwk":""
}
```

License
----
MIT