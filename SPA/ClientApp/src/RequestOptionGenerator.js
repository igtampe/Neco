export const GenerateJSONPost = (SessionID, Body) => {
    return({
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json', 
            'SessionID': SessionID },
        body: JSON.stringify(Body)
    })
}

export const GenerateJSONPut = (SessionID,Body) => {
    return({
        method: 'PUT',
        headers: { 
            'Content-Type': 'application/json', 
            'SessionID': SessionID},
        body: JSON.stringify(Body)
    })
}


export const GenerateGet = (SessionID) => {    
    return(SessionID ? {
        method: 'GET',
        headers: {'SessionID': SessionID },
    } : { method: 'GET'})
}

export const GenerateDelete = (SessionID) => {    
    return({
        method: 'DELETE',
        headers: {'SessionID': SessionID },
    })
}


//Honestly I would probably just copy the picture picker so no need for a generate request with files