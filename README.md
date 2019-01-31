# GloomHaven Api (C#)

A rest API used to to persist characters and associated modifier decks for the GloomHaven board game. This is only used by me and some friends so it has a simple authentication system. 

## Tech Stack

- ASP.NET Core 2.2
- [PostgreSQL](https://www.postgresql.org/) : a relational database
- [Dapper](https://github.com/StackExchange/Dapper) : a micro-orm also used by StackOverflow 

## API 

| Category  | API |
| -- | -- |
| Authentication | [Login](#Login) |
|  | [Logout](#Logout) |
|  | [Change Password](#Change-Password) |
| Game Data | [List Classes](#List-Classes) |
|  | [Get Class](#Get-Class) |
| Characters | [List Characters](#List-Characters) |
|  | [Get Character](#Get-Character) |
|  | [Add Character](#Add-Character) |
|  | [Update Character](#Update-Character) |
|  | [Patch Character](#Patch-Character) |
| Modifier Decks | [Get Deck](#Get-Deck) |
|  | [Draw Card](#Draw-Card) |
|  | [Reshuffle Deck](#Reshuffle-Deck) |

---

## Headers

Apart from `login`, all API calls require the access token returned in the login response to be sent in the `Authorization` header. Specifically, these are the required headers.

| Name  | Value | Description |
| --- | --- | --- |
| Content-Type | application/json | Required for all requests |
| Authorization | Access token from login response | Required for all requests, except `login` |

---

## Login

`https://[path-to-api]/authentication/login`

Method: POST

Request Type:
```c#
{
    email    : string // Email of the user
    password : string // Password of the user
}
```

Example Request:
```json
{ 
    "email": "example.user@example.com", 
    "password": "examplepassword" 
}
```

Response type:
```c#
{    
    email : string // Email of the user     
    accessToken : string // Access token for the `Authorization` header 
    accessTokenExpiresAt : DateTime // Expiry date for the access token 
}
```

Example Response:
```json
{
    "email": "example.user@example.com",
    "accessToken": "Bearer 9a44b657-d953-45de-9685-de1df239094e",
    "accessTokenExpiresAt": "2020-01-02T00:00:00+11:00"
}
```

---

## Logout

`https://[path-to-api]/authentication/logout`

Method: DELETE

Parameters: None

Response: 204 No Content

---

## Change Password

`https://[path-to-api]/authentication/password`

Method: POST

Request Type:
```c#
{
    oldPassword : string // Current password
    newpassword : string // New password
}
```

Example Request:
```json
{ 
    "oldPassword": "oldpassword", 
    "newpassword": "newpassword" 
}
```

Response: 204 No Content

---

## List Classes

`https://[path-to-api]/game/classes`

Method: GET

Parameters: None

Response type:
```c#
// Top level
{    
    data : GloomClass[] // Array of GloomClass elements
}

// GloomClass 
{    
    className  : string // Unique name/id of the class    
    name       : string // Display name    
    symbol     : string // Icon    
    isStarting : bool   // True if the class is available when starting a game    
    perks      : Perk[] // Array of available perks for this class
    xpLevels    : int[] // Array of xp required to reach next level
    hpLevels    : int[] // Array of health pool levels per xp level
    petHPLevels : int[] // Pet health pool levels (only for Beast Tyrant)
}

// Perk 
{    
    id       : string // Unique id of the perk    
    quantity : int    // Number of times that this perk can be claimed   
    actions  : string // Action to perform for this perk  
}
```

Example Response:
```c#
{
    "data": [
        {
            "className": "Brute",
            "name": "Inox Brute",
            "symbol": "Horns",
            "isStarting": true,
            "perks": [
                {
                    "id": "brt01",
                    "quantity": 1,
                    "actions": "Remove two -1 cards"
                },
                ...//more perks
            ],
            "xpLevels": [0,45,95,150,210,275,345,420,500],
            "hpLevels": [10,12,14,16,18,20,22,24,26]
        },
        {
            "className": "Tinkerer",
            "name": "Quatryl Tinkerer",
            "symbol": "Cog",
            "isStarting": true,
            "perks": [
                {
                    "id": "tnk01",
                    "quantity": 2,
                    "actions": "Remove two -1 cards"
                },
                ...//more perks
            ],
            "xpLevels": [0,45,95,150,210,275,345,420,500],
            "hpLevels": [8,9,11,12,14,15,17,18,20]
        }
        ...//more classes
    ]
}
```

---

## Get Class

`https://[path-to-api]/game/classes/{className}`

Method: GET

Parameters: 
| Name  | Value | 
| --- | --- | 
| className | The unique className as described in the [List Classes](#List-Classes) section |

Response type:
```c#
// GloomClass 
{    
    className  : string // Unique name/id of the class    
    name       : string // Display name    
    symbol     : string // Icon    
    isStarting : bool   // True if the class is available when starting a game    
    perks      : Perk[] // Array of available perks for this class
    xpLevels    : int[] // Array of xp required to reach next level
    hpLevels    : int[] // Array of health pool levels per xp level
    petHPLevels : int[] // Pet health pool levels (only for Beast Tyrant)
}
}

// Perk 
{    
    id       : string // Unique id of the perk    
    quantity : int    // Number of times that this perk can be claimed   
    actions  : string // Action to perform for this perk  
}
```

Example Request: 

`https://[path-to-api]/game/classes/brute`

Example Response:
```c#
{
    "className": "Brute",
    "name": "Inox Brute",
    "symbol": "Horns",
    "isStarting": true,
    "perks": [
        {
            "id": "brt01",
            "quantity": 1,
            "actions": "Remove two -1 cards"
        },
        ...//more perks
    ],
    "xpLevels": [0,45,95,150,210,275,345,420,500],
    "hpLevels": [10,12,14,16,18,20,22,24,26]
}
```

---

## List Characters

`https://[path-to-api]/characters`

Method: GET

Parameters: None

Response type:
```c#
// Top level
{    
    data : Character[] // Array of Character elements
}

// Character 
{    
    id         : int    // Unique id of the character
    name       : string // Unique name in case you have two of the same classes    
    className  : string // Unique name/id of the class    
    experience : int    // Amount of experience the character has     
    level      : int    // Current level (based on experience)
    gold       : int    // Amount of gold the character has   
}
```

Example Response:
```json
{
    "data": [
        {
            "id": 1,
            "name": "My Brute",
            "className": "Brute",
            "experience": 11,
            "level": 1,
            "gold": 44
        },
        {
            "id": 42,
            "name": "My Cragheart",
            "className": "Cragheart",
            "experience": 55,
            "level": 2,
            "gold": 47
        }
    ]
}
```

---

## Get Character

`https://[path-to-api]/characters/{characterId}`

Method: GET

Parameters: 
| Name  | Value | 
| --- | --- | 
| characterId | The id of the character |

Response type:
```c#
// GloomClass 
{    
    id           : int    // Unique id of the character
    name         : string // Unique name in case you have two of the same classes    
    className    : string // Unique name/id of the class    
    experience   : int    // Amount of experience the character has   
    level        : int    // Current level (based on experience)
    hp           : int    // Current HP (based on level) 
    petHP        : int    // Nullable. Current pet HP (only for Beast Tyrant)
    gold         : int    // Amount of gold the character has   
    achievements : int    // Amount of gold the character has  
    claimedPerks : Perk[] // Array of claimed perks 
}

// Perk 
{    
    id       : string // Unique id of the perk    
    quantity : int    // Number of times that this perk has been claimed   
    actions  : string // Action to perform for this perk  
}
```

Example Request: 

`https://[path-to-api]/characters/42`

Example Response:
```json
{
    "id": 42,
    "name": "My Cragheart",
    "className": "Cragheart",
    "experience": 55,
    "level": 2,
    "hp": 12,
    "gold": 47,
    "achievements": 25,
    "perks": [
        {
            "id": "crg02",
            "quantity": 1,
            "actions": "Remove one -1 card and add one +1 card"
        },
        {
            "id": "crg05",
            "quantity": 1,
            "actions": "Add one +2 MUDDLE card"
        },
        {
            "id": "crg10",
            "quantity": 1,
            "actions": "Ignore negative scenario effects"
        }
    ]
}
```

---

## Add Character

`https://[path-to-api]/characters`

Method: POST

Request Type: 
```c#
{
    name      : string // Character name (unique in case of multiple chars of the same class)
    className : string // The class name of the character
}
```

Response code: `201 Created`

Response type: Same type as returned for [Get Character](#Get-Character)

Example Request: 
```json
{ 
    "name": "Test Tinkerer",
    "className": "Tinkerer" 
}
```

---

## Update Character

`https://[path-to-api]/characters/{characterId}`

Method: POST

Request Type: 
```c#
// Character update - ALL fields are required
{
    name         : string
    experience   : int
    gold         : int
    achievements : int 
    perks        : Perk[]
}

// Perk - this list of perks will replace the existing perks
{
    id       : string // Id of the perk (from the game data)
    quantity : int // Number of time the perk has been claimed
}
```

Response code: `204 NoContent`

Example Request: 
```json
{
    "Name": "Test Brute",
    "Experience": 11,
    "Gold": 44,
    "Achievements": 22,
    "Perks": [
    	{ "Id": "brt02", "Quantity": 2 },
    	{ "Id": "brt03", "Quantity": 1 },
    	{ "Id": "brt04", "Quantity": 1 }
    ]
}
```

---

## Patch Character

`https://[path-to-api]/characters/{characterId}`

Method: PATCH

Request Type: 
```c#
// Character patch - ALL fields are optional
{
    name         : string 
    experience   : int
    gold         : int
    achievements : int 
    perks        : Perk[]
}

// Perk - this list of perks will replace the existing perks
{
    id       : string // Id of the perk (from the game data)
    quantity : int // Number of time the perk has been claimed
}
```

Response code: `204 NoContent`

Example Request: 
```json
{
    "Name": "Test Brute",
    "Experience": 11,
    "Perks": [
    	{ "Id": "brt02", "Quantity": 2 },
    	{ "Id": "brt03", "Quantity": 1 },
    	{ "Id": "brt04", "Quantity": 1 }
    ]
}
```

---

## Get Deck

`https://[path-to-api]/characters/{characterId}/decks`

All characters have exactly one modifier deck.

Method: GET

Parameters: 
| Name  | Value | 
| --- | --- | 
| characterId | The id of the character |

Response type:
```c#
// Deck
{    
    totalCards  : int    // Number of cards in the deck (changes with perks and xp)
    currentCard : Card   // Current card... nothing if no cards have been drawn
    discards    : Card[] // Array of discarded cards
}

// Card 
{    
    damage       : int  // Damage to inflict
    drawAnother  : bool // True if another card should be drawn after playing the card
    reshuffle    : bool // True if the deck should be reshuffled after playing the card
    action       : string // Action to perform (eg: heal)
    actionAmount : int option // Amount associated with the action (eg: Heal 2)
}
```

Example Request: 

`https://[path-to-api]/characters/42/decks`

Example Response:
```json
{
    "totalCards": 21,
    "currentCard": {
        "damage": 1,
        "drawAnother": false,
        "reshuffle": false,
        "action": "Damage"
    },
    "discards": [
        {
            "damage": 1,
            "drawAnother": false,
            "reshuffle": false,
            "action": "Damage"
        },
        {
            "damage": 1,
            "drawAnother": false,
            "reshuffle": false,
            "action": "Damage"
        }
    ]
}
```

---

## Draw Card

`https://[path-to-api]/characters/{characterId}/decks`

This is probably not the most "restful" api method. It is a post because it modifies data on the server. This and reshuffle have more of an intrinsic rpc nature. So rather than being a rest purist, I made them more rpc-like.

Method: POST

Request type:
```c#
{    
    action  : string // Either "Draw" or "Reshuffle"
}
```

Response type: same as for [Get Deck](#Get-Deck) 

---

## Reshuffle Deck

`https://[path-to-api]/characters/{characterId}/decks`

Method: POST

Request type:
```c#
{    
    action  : string // Either "Draw" or "Reshuffle"
}
```

Response type: same as for [Get Deck](#Get-Deck) but the discards will be empty and there will be no current card.

---