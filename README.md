# The-Delta-Code-Assault
Overview:
Like the original arcade game back in 90s, this is a 2D asteroid and space invaders. The goal is to survive and defeat the boss. You may get points if defeated by enemy and boss. There are some unique to enemies that are different than original games and your player itself. First, enemy can now shoot in various type of bullet. Second, instead of asteroid, there could be different object that player must avoid hitting (thus called Delta Code Assault – where destroy all enemy are great way to reduce death). The original asteroid does not have collision and space invaders are only moving horizontal and only if down when reaches edge. This one is random direction to move rather than basic cardinal such as quadratic path, cubic path, etc…. It can spawn in sideway or up down from the edge as well for unique enemy. I want to see different a bit than original game as I watched gameplay. If enemy collides each other, they just bounce each other. If player shot hits enemy, it would reduce the health until reaches zeroes. Bullet and objects do not collide each other. Finally, the player can have some bonus for unique enemy when killing such as extra points, faster fire rate for few seconds, temporarily invincible, etc… Most of them are just regular enemy like object went any direction and enemy can shoot at one direction. Unique enemy can shoot a bullet in unique direction rather than straight down. Touching the bullet or collide objects will lose a life. You will gain 3 seconds invincible when respawn until game over. The background itself moves up at a constant speed kind of like Raiden X. The player only has one weapon to shoot or using secondary ability to switch your player move slightly slower, but the shot goes slower as well. This should allow player has some unique for different gameplay.
Controls:
This game is only using keyboard.

•	Fire: Press Spacebar to shoot, damaging anything along its path

•	Move: Using WASD to move your player as if using original asteroid game, but this is no rotation player. It only moves up down or sideway (including the diagonal)

•	Focus or Unfocus mode: Press Left Shift to switch ability. Unfocus mode is regular speed and shoot faster; hit enemy does regular damage.  Focus mode is slower speed a bit and shoot slightly slower but does increase damage. However, small delay about 3-5 seconds to avoid spam each switch.

Art Assets:

•	Objects: This can be any object, most commonly the junk object, regular enemy, some unique enemy, and boss.

•	Player: Just a basic player’s ship. A circle/sphere shape is a bullet, player shot. Shield is just a circle or ellipse.

•	Bullet: Just a simple sphere or simple laser.

•	HUD: Represents player’s score and lives

Audio Assets:

•	Bullet: Projectile firing

•	Player: Death, Mode Type

•	Object: Collide

•	Game: Game Start, Restart, Level Reset, Game Over

•	Enemy: Killed

Game Flow:
•	Starting point: Player at the bottom screen.

•	Junk object will spawn at the top screen as time progress.

•	Shooter enemy will spawn around ¼ of time progress, starting easy way to dodge.

•	Starting as little health so player can prepare the worst.

•	As soon as time progress, more objects and enemy shooter spawn more in any direction on from the edge. In addition, more bullets will appear making harder to dodge a bit. The health point also increases more each time.

•	When around end of time progress, boss will appear as enemy shooter but more complicated such as unique shoot, spawn helper enemy shooter and junk objects. Some bullets do went homing at you but only in downward direction with any angle with slower speed horizontal.

•	When boss defeated, you win, you may restart new game if you want with displaying victory screen.

•	When game over, it is the same as boss defeated but displaying game over screen.

Challenge that may concern:

•	Enemy can spawn too much that lead O(n^2), so reduce to 5-10 enemy shooter, and another 5-10 junk objects. It should be no more than 25 enemies at one screen.

•	Bullet can lag too much if not careful, it only shoots a general path to reduce lag game. Maybe up to 100 bullets at one screen. Same thing O(n^2).

•	Objects collide too much: All objects should expect move in average speed not too fast cause player frustration. It starts slow speed and then little bit faster each time progress until near boss screen.

•	Maybe too complicated homing bullet, it needs follow player position and might expect not to be correctly homing but more on regular style.

•	Background loop each time reaches at the end to loop back at the bottom edge.


The CHANGES here:
1. Not implemeneted Focus and Unfocus Mode
2. Boss not implemented -> too complicated and unfinished complex pattern
3. Start and game over screen not implemeneted yet
4. HUD is not available -> attempt to do but failed to work
5. Only the player shot, only certain enemy, and enemy shot works.
