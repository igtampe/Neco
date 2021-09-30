# Neco Backend
This is the Neco Backend, which handles all operations for Neco. The aim of the game is to try to make the Backend as smart as possible so we have the frontend do *nada*. See the controllers for more details.


## Sessions and the Session Manager
On this level though, we can talk about the Session Manager and its sessions. In order to ensure users are authorized to commit commands, and to ensure users do not locally store their credentials as a form of authorization to do operations, we have a session manager to hand out Sessions (Identified with GUIDs) and keep track of which one is tied to which users. In this way, the frontend only needs to store the session ID, rather than credentials, to stay authorized.

The Session manager also can verify if sessions are expired, defined as 15 minutes since their last action. Any time a session is requested, the session manager updates a session's expiration date. It also runs a separate thread to check every minute for expired sessions, and to remove them as needed.

In order to have consistency, the Session Manager is a singleton, accessed through a static property, and thus can be accessed by any controller.

### Session
A Session is comprised of the following fields and methods

#### Fields:
|Name|Description|
|-|-|
|ID|GUID of the session|
|Expiration Date|Date and time this session will expire|
|UserID|ID this session is tied to|

#### Methods:
|Method|Description|
|-|-|
|ExtendSession()|Extends the session if it's not already expired|

### Session Manager
The Session manager is comprised of the following fields and methods

#### Fields:
|Name|Description|
|-|-|
|Count|Number of active sessions|

#### Methods:
|Method|Description|
|-|-|
|Login()|Logs in a user with given User ID, and returns a session ID|
|FindSession()|Finds a session. Extends, then returns it if it is not expired and was found. Removes it if it was found but was expired|
|ExtendSession()|Actually not necessary anymore since FindSession() extends sessions. Huh... will need to verify this|
|Logout()|Removes given session ID from the list of active sessions (effectively logging out the user)|
|LogOutAll()|Removes all sessions that have matching given userID (Effectively logging the user out of every active session)|
|RemoveExpiredSessions()|Removes all expired sessions from the list of active sessions|
