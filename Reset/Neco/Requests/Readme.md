# Neco Requests
This folder holds requests to the backend for operations on core neco operations, it also includes INecoRequest, an interface for every neco request, which only contains a session ID

|Request|Description|Included Data|
|-------|-----------|-------------|
|Bank Account Action Request|Request to commit some action on a bank account (Like close it, or view its logs)|Session ID, Bank Account ID|
|Bank Account Open Request|Request to open a bank account|Session ID, Bank Account Type ID|
|Certification Request|Request to certify some text|Session ID, Text to certify|
|Checkbook Request|Request to send a Checkbook item|Session ID, Attatched Transaction Request, Checkbook Item Type, Variant, Comment|
|New User Request|Request to create a new user|Name, Pin User Type|
|Password Change Request|Request to change a user's password|Session ID, Current Password, New Password|
|Transaction Request|Request to commit a transaction|Session ID, Name, From Bank ID, To Bank ID, Amount|

