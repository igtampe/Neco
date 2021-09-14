﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Igtampe.Neco.Backend {

    /// <summary>Session Manager for Neco</summary>
    public class SessionManager {

        /// <summary>Internal Singleton Session Manager object</summary>
        private static SessionManager SingletonSM;

        /// <summary>Gets the static, singleton session manager for Neco</summary>
        public static SessionManager Manager { 
            get {
                if (SingletonSM == null) { SingletonSM = new SessionManager(); }
                return SingletonSM;
            } 
        }

        /// <summary>Collection of all sessions in this manager</summary>
        private readonly ICollection<Session> Sessions;

        /// <summary>Amount of sessions in the collection (including those that are expired)</summary>
        public int Count { get { return Sessions.Count; } }

        /// <summary>Internal constructor to create a session manager</summary>
        private SessionManager() { Sessions = new HashSet<Session>(); }

        /// <summary>Adds given session to the sessions collection if it is not expired</summary>
        /// <param name="S"></param>
        public void AddSession(Session S) {
            if (S.Expired) { throw new ArgumentException("Session is Expired"); }
            Sessions.Add(S);
        }

        /// <summary>Returns a session with sepcified ID. 
        /// If the Session is expired, it returns null, and removes the session from the collection.
        /// Otherwise, it extends the session before returning it.</summary>
        /// <param name="ID"></param>
        /// <returns>Returns a session if one exists, if not NULL</returns>
        public Session FindSession(Guid ID) {
            Session S = Sessions.FirstOrDefault(S => S.ID == ID);
            if (S == null) { return null; }
            if (S.Expired) { Sessions.Remove(S); return null; }
            S.ExtendSession();
            return S;
        }

        /// <summary>Extends a session with given UID</summary>
        /// <returns>True if a session was found and it was able to be extended. False otherwise</returns>
        public bool ExtendSession(Guid ID) {
            Session S = FindSession(ID);
            if (S == null) { return false; }
            S.ExtendSession(); //this *probably* doesn't save it but we will need to check it.
            return true;
        }

        /// <summary>Removes a session with specified ID</summary>
        /// <param name="ID"></param>
        /// <returns>Returns true if the session was found and was removed, false otherwise</returns>
        public bool RemoveSession(Guid ID) {
            Session S = FindSession(ID);
            if (S == null) { return false; }
            return Sessions.Remove(S);
        }

        /// <summary>Removes all expired sessions from the collection of active sessions</summary>
        /// <returns>Amount of removed sessions</returns>
        public int RemoveExpiredSessions() {
            ICollection<Session> ExpiredSessions = Sessions.Where(S => S.Expired).ToHashSet();
            int RemovedCount = 0;
            foreach (Session S in ExpiredSessions) { if (Sessions.Remove(S)) RemovedCount++; }
            return RemovedCount;
        }

        /// <summary>Routine to periodically remove all expired sessions from the collection</summary>
        public static void SessionRemoverThread(int Seconds) {
            while (true) {
                Thread.Sleep(Seconds * 1000);
                if (Manager.Sessions.Count == 0) { continue; }
                Console.WriteLine($"Removed {Manager.RemoveExpiredSessions()} expired session(s)");
            }
        }

    }
}
