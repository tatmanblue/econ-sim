# Consumerism Market AI
Consumerism the market AI for the game.  

# Components
Here's a list of parts so far
| Name            | Role            |
| --------------- | --------------- |
| Producer        | Creates "items" that become part of the economy. This means item qty on the economy increases but it doesn't mean the producer put the item on the market.  Is most likely a seller but doesn't have to be |
| Consumer        | Consumes "items" that are in the economy.  Is most likely a buyer but doesn't have to be |
| Buyer           | Buys an item off the market but doesn't remove it from the economy  |
| Seller          | Sells an item to the market but doesn't increase total qty of the item in the economy |
| Supply Side Mgr | Handles Producer/Seller interactions with the market |
| Demand Side Mgr | Handles Consumer/Buyer interactions with the market |
| "Items"         | Stuff that goes into the market |
| EconomicItem    | aka "Items" |
| EconomicItemDesc | Data that describes how producers and consumers affect and are affected by changes in quanities of EconomicItem |


# Notes
https://brilliant.org/wiki/supply-and-demand/
