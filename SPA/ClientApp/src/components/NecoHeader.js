import { Typography } from "@mui/material";
import React, { useState } from "react";

function NecoHeaderGreeting(props) {

    const [greeting,setGreeting] = useState(undefined);
    const [question, setQuestion] = useState(false);
    const [loading, setLoading] = useState(false);

    const AnyGreetings = [
        'Hello', 'Hi', 'Welcome back', 'Allo', 'Salutations', 'Aloha', 'Greetings', 'Shalom'
    ]

    const MorningGreetings = [ 'Good morning', 'Bonjour', 'Buenos Dias', 'Top o\' the morning to ya' ]

    const DayGreetings = ['Good Day', 'Bonjour', 'Buenas tardes']

    const EveningGreetings = ['Good evening', 'Bonsoir', 'Buenas tardes']

    //There are no night greetings because every night greeting isn't really a greeting it's a farewell.
    //Who the heck do you know that starts a midnight conversation like 'GOOD NIGHT MR. JOHNSON FAIR WEATHER WE'RE HAVING AIN'T IT?'

    //For funsies though let's jab at those late night people (AKA myself). These will only show up from 1-5 AM
    const UltraLateNightGreetings = ['Bit late isn\'t it', 'Heading to bed soon', 'Maybe go to sleep soon', 'Isn\'t it past your bedtime', 'What now', 
    'What kind of late night transaction are you up to', 'Is this really necessary now']

    //Ripped from the MDN Web Docs
    function getRandomInt(max) { return Math.floor(Math.random() * max); }

    const GetGreetingFromCollection = (collection) => {

        //assume we already have the collection
        setGreeting(collection[getRandomInt(collection.length)])

    }

    //Any should contain an image that's usable at any time of day (like an interior or underground shot)
    
    if(!greeting && !loading){

        //Flip a weighted coin to determine if to take from the any set or the time of day set
        //Unlike images, we favor more the any. 60% for Any

        //EXCEPT: if the current hour is after 1 and before 5 we jump straight to getting from ultra late night
        var hour = (new Date()).getHours();
        if(1 <= hour && hour < 5) { 
            setQuestion(true)
            GetGreetingFromCollection(UltraLateNightGreetings); 
            return; 
        }

        setLoading(true)

        var Any = getRandomInt(10) < 6;
        if(Any) { GetGreetingFromCollection(AnyGreetings); return; } else {

            //Unlike images, morning greetings go from 5 to 12 midday
            if(5 <= hour && hour < 12 )  { GetGreetingFromCollection(MorningGreetings); return; }

            //Day goes from 12 to 5
            if(12 <= hour && hour < 17) { GetGreetingFromCollection(DayGreetings); return; }

            //and evening for anything else
            GetGreetingFromCollection(EveningGreetings);

        }
    }

    return (
        <div style={props.style}>
            <Typography sx={{fontFamily:'outfit', color:'white', fontSize:'200%'  }}>
                {greeting ? greeting + (question ? ', ' : ' ') + props.name + (question ? '?' : '!') : '...'}
            </Typography>
        </div>
    );

}


export default function NecoHeader(props) {

    const [src,setSrc] = useState(undefined);
    const [alt,setAlt] = useState(undefined);

    const [loading, setLoading] = useState(false);

    //Ripped from the MDN Web Docs
    function getRandomInt(max) { return Math.floor(Math.random() * max); }

    const GetImageFromCollection = (collection) => {
        fetch('/Images/Headers/' + collection + '/index.json')
        .then(response=>response.json())
        .then(data=>{

            console.log('Fetched!')
            //data should be an array with lenght. Select a random one
            var img = data[getRandomInt(data.length)]
            
            //ok we should now have a random image.
            setSrc('Images/Headers/' + collection + '/' + img.Name)
            setAlt(img.Description)

        })
        .catch(
            e=> {
                console.log("Uh oh spaghetti oh-s")
                console.error(e)
            }
        )
        .finally( E => {
            console.log('Adios!')
            setLoading(false) }
            )
    }

    //Any should contain an image that's usable at any time of day (like an interior or underground shot)
    const GetAny = () => {GetImageFromCollection('Any')}

    const GetCurrent = () => {

        //Determine what the current time of day is
        var hour = (new Date()).getHours();

        //Any hour between 8 AM (8) to 5 PM (17) is Day
        //Any hour from 5 AM (5) to 8 AM (8) is Sunrise
        //Any hour from 5 PM (17) to 8 PM (20) is Sunset
        //Anything else is night

        if(8 <= hour && hour < 17) { GetImageFromCollection('Day'); return; }
        if(5 <= hour && hour < 8 )  { GetImageFromCollection('Sunrise'); return; }
        if(17 <= hour && hour < 20)  { GetImageFromCollection('Sunset'); return; }
        GetImageFromCollection('Night')

    }

    if(!src && !loading){

        //Flip a weighted coin to determine if to take from the any set or the time of day set
        //Let's do 70% time of day shots, 30% any shots

        setLoading(true)
        console.log('Time to load an image!')

        var Any = getRandomInt(10) < 3;
        if(Any) { GetAny();} else {GetCurrent();}
    }

    return (
        <div style={{marginBottom:'15px', position:'relative'}}>
            <img src={src ? src : '/Images/Headers/Placeholder.jpg'} alt={alt ? alt : 'Getting...'} width='100%'/><br/>
            <NecoHeaderGreeting style={{position:'absolute', bottom:'15%', left:'16px', textShadow:'2px 2px 8px #000000'}} name={props.name}/>
            <Typography sx={{color:'text.secondary', fontSize:10, fontFamily:'outfit' }}><i>{alt ? alt : 'Getting image...'}</i></Typography>
        </div>
    );

}
