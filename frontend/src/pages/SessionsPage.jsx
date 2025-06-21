import { useState } from "react";
import { useUser } from "../contexts/UserContext";

function Sessions()
{
    const [userSessions, setUserSessions] = useState("");
    const [adminSessions, setAdminSessions] = useState("");
    const [moderatedSessions, setModeratedSessions] = useState("");
    const { user } = useUser();
    console.log(user);
    console.log("I am in sessions");

}

export default Sessions;