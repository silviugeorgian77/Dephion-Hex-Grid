# Dephion-Hex-Grid

## Acceptance Criteria
- The hexagons should be implemented in 3D
- The hexagon grid data should be retrieved from the backend specified endpoint o The whole data flow from backend retrieval to the actual visual implementation
should be done according to your best knowledge of OOP and design pattern
strategies
- The hexagon grid should be drawn according to the backend data specification o Clicking one of the drawn hexagons, should raise the hexagon tile in a smooth way and change the hexagon tile’s colour
- Changing the tile’s colour, should happen in a smooth transition from the current colour to the new one
- Clicking on a hexagon tile will lower all other tiles to their original height o The main camera view should be from an angled perspective over the 3D
hexagon grid

# Architecture

## Data Provider
- Responsible for providing the newest hex grid data
- Priorities of the data:
  - Online data
  - Last online data
  - Data that shipped with the app
When new online data is received, it gets cached. Next time, if no internet, the last available data is used.
If the app never had internet, when we open it, it uses by default the pre-shipped data.

## Hex Grid Manager
- Can be considered the scene manager.
- Responsible for linking the data to the views.
- Pulls data from **Data Provider** and feeds it to **Hex Grid Displayer**.

## Hex Grid Displayer
- Receives the hex grid data.
- Gives the data to the **Hex Grid Builder** and displays the output.
- Responsilve for display the data and managing input.

## Hex Grid Builder
- This is responsible for creating the view model of the hex grid.
- There are multiple ways of creating a hex grid, that is why we should keep the **Hex Grid Displayer** and the **Hex Grid Builder** separated.
- In the future we can have multiple **Hex Grid Builder** variants and swap them easily.

## Animation Utilities
- We can either using Unity Animation system, or animate the programatically way. In both cases we need to allocate resources for animation behaviour logic.
- In the case of programmatical animations, we can reuse this approach easily in other scenarios.

## Input Utilities
- This are required in order to provide basic input callbacks. For instance, we need to know when the user releases the mouse button, so we can animate the hex item accordingly

![Architecture](/hex%20grid.jpg?raw=true)
