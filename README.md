# Doop Doop Roop

**All of the following is subject to change!**

## Current Prototype
The current playable prototype contains an top down isometric scene with blue enemies and a red player. Enemies and the player cannot move to spaces occupied by another enemy or player.

## Controls
WASD will move the player in the 4 cardinal directions by a fixed amount of distance. The white square at the bottom of the screen will flash green when inputs to move the player are valid.

Valid input windows are based on the BPM of the currently playing track.

## Music
The current implementation of the BPM detection system requires manual input of a song's BPM. See the current queue of tracks in the Drivers->GameDriver GameObject in the hierarchy view of the editor. Music will play according to the order in that queue.
