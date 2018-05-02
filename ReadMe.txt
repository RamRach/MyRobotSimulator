Toy Robot Simulator
We use this test as an indication of the kind of code that a candidate would write on a day to
day basis, so please take your time and submit representative code.
Using C#, design and code up a Toy Robot Simulator. Your code must include unit tests.
Host your code on Github and send in your repo URL for review. When setting up the repo,
structure it such that it will be used as a working repo in the future. You should aim to deliver
production ready code, and your repository should be structured as you would if you were
setting up a real repository.
Description
- The application is a simulation of a toy robot moving on a square tabletop, of
dimensions 5 units x 5 units.
- There are no other obstructions on the table surface.
- The robot is free to roam around the surface of the table, but must be prevented from
falling to destruction. Any movement that would result in the robot falling from the
table must be prevented, however further valid movement commands must still be
allowed.
The application should be able to read in any one of the following commands:
PLACE X,Y,F
MOVE
LEFT
RIGHT
REPORT
- PLACE will put the toy robot on the table in position X,Y and facing NORTH, SOUTH,
EAST or WEST.
- The origin (0,0) can be considered to be the SOUTH WEST most corner.
- The first valid command to the robot is a PLACE command, after that, any sequence
of commands may be issued, in any order, including another PLACE command. The
application should discard all commands in the sequence until a valid PLACE
command has been executed.
- MOVE will move the toy robot one unit forward in the direction it is currently facing.
- LEFT and RIGHT will rotate the robot 90 degrees in the specified direction without
changing the position of the robot.
- REPORT will announce the X,Y and F of the robot. This can be in any form, but
standard output is sufficient.
- A robot that is not on the table should ignore the MOVE, LEFT, RIGHT and REPORT
commands.
- Input can be from a file, or from standard input, as the developer chooses.
- Provide test data to exercise the application.
Constraints:
- The toy robot must not fall off the table during movement. This also includes the initial
placement of the toy robot.
- Any move that would cause the robot to fall must be ignored.
Here is some example input and output:
a)
PLACE 0,0,NORTH
MOVE
REPORT
Output: 0,1,NORTH
b)
PLACE 0,0,NORTH
LEFT
REPORT
Output: 0,0,WEST
c)
PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT
Output: 3,3,NORTH


Implementation

RobotCommander is the commander class that accepts Commands from the user, parses the commands and passes on to Robot Action class 
RobotAction is the actual class where all the actions are actioned up on
An error is captured with proper messasges when unallowed or invalid commands are passsed
