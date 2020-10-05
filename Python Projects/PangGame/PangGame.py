import os
import pygame

########################################################################################################
#                                   Basic initialization (Must do it)                                  #
########################################################################################################

pygame.init()       # initialization (Must do it)

# Set Window Size
screen_width = 900
screen_height = 550

screen = pygame.display.set_mode((screen_width, screen_height))

# Set Screen Title
pygame.display.set_caption("Pang Game_Rene's project")  # Game Name

# FPS
clock = pygame.time.Clock()

########################################################################################################
#           User game initialization (Background, Game image, Coordinate, Speed, Font etc)             #
########################################################################################################

current_path = os.path.dirname(__file__)                # Returns the location of the current file
image_path = os.path.join(current_path, "images")       # Returns the location of the images folder

########################################### Background image ########################################### 
background = pygame.image.load(os.path.join(image_path, "background.jpg"))

################################################ Stage #################################################
stage = pygame.image.load(os.path.join(image_path, "stage.png"))

stage_size = stage.get_rect().size          # Get stage size
stage_height = stage_size[1]

########################################### Character (Sprite) ##########################################
character = pygame.image.load(os.path.join(image_path, "character.png"))

character_size = character.get_rect().size          # Get character size
character_width = character_size[0]                 # Size of the character width size
character_height = character_size[1]

# Set the character's Location at half of the width size in the screen
character_x_pos = (screen_width / 2) - (character_width / 2) 
character_y_pos = screen_height - character_height - stage_height  # At the end of the screen

# Move Coordinate
character_to_x_LEFT=0
character_to_x_RIGHT=0

# Move Speed
character_speed = 5

################################################ Weapon ################################################
weapon = pygame.image.load(os.path.join(image_path, "weapon.png"))

weapon_size = weapon.get_rect().size
weapon_width = weapon_size[0]

# Weapon can shoot multiple times
weapons = []

weapon_speed = 100

################################################# Ball #################################################
ball_images = [
    pygame.image.load(os.path.join(image_path, "balloon1.png")),
    pygame.image.load(os.path.join(image_path, "balloon2.png")),
    pygame.image.load(os.path.join(image_path, "balloon3.png")),
    pygame.image.load(os.path.join(image_path, "balloon4.png"))
]

ball_speed_y = [-18, -15, -12, -9] 

balls = []

# First ball append
balls.append({
    "pos_x" : 50,       # x coordinate of a ball
    "pos_y" : 50,
    "img_idx" : 0,      # index of a ball
    "to_x" : 3,         # direction of x-axis movement
    "to_y" : -6,        # if the number is -3, it will move to left.
    "init_spd_y" : ball_speed_y[0]      # Fist speed
})

# Weapon, ball which will be removed
weapon_to_remove = -1
ball_to_remove = -1

################################################# Font #################################################

game_font = pygame.font.Font(None, 40)
total_time = 100
start_ticks = pygame.time.get_ticks()   

# Game Over message
# TimeOut, Mission Complete, Game Over
game_result = "Game Over"

########################################################################################################
#                                       Event (Keybord, mouse etc)                                     #
########################################################################################################

# Event loof
running = True      # Is this game running now?
while running:
    dt = clock.tick(80)                 

    for event in pygame.event.get():    # What kind of event has happened?
        if event.type == pygame.QUIT:   # Has an event happened to close the window?
            running = False             # The game was finished

        if event.type == pygame.KEYDOWN:    # Check puch the key? or not
            if event.key == pygame.K_LEFT:
                character_to_x_LEFT -= character_speed
            elif event.key == pygame.K_RIGHT:
                character_to_x_RIGHT += character_speed 
            elif event.key == pygame.K_SPACE:
                weapon_x_pos = character_x_pos + (character_width / 2) - (weapon_width / 2)
                weapon_y_pos = character_y_pos
                weapons.append([weapon_x_pos, weapon_y_pos])

        if event.type == pygame.KEYUP:
            if event.key == pygame.K_LEFT:
                character_to_x_LEFT = 0
            elif event.key == pygame.K_RIGHT:
                character_to_x_RIGHT = 0

########################################################################################################
#                                       Character Position / boundary                                 #
########################################################################################################  

    character_x_pos += character_to_x_LEFT + character_to_x_RIGHT

    # Character screeen boundary
    if character_x_pos < 0:
        character_x_pos = 0
    elif character_x_pos > screen_width - character_width:
        character_x_pos = screen_width - character_width

    ############################################## Weapon ##############################################
    weapons = [ [w[0], w[1] - weapon_speed] for w in weapons]

    # Remove weapons
    weapons = [ [w[0], w[1]] for w in weapons if w[1] > 0]

    ##################################### Definition Ball location #####################################
    for ball_idx, ball_val in enumerate(balls):
        ball_pos_x = ball_val["pos_x"]
        ball_pos_y = ball_val["pos_y"]
        ball_img_idx = ball_val["img_idx"]

        ball_size = ball_images[ball_img_idx].get_rect().size
        ball_width = ball_size[0]
        ball_height = ball_size[1]

        # The effect of bouncing off a width wall
        if ball_pos_x < 0 or ball_pos_x > screen_width - ball_width:
            ball_val["to_x"] = ball_val["to_x"] * -1

        # The effect of bouncing off a height wall
        if ball_pos_y >= screen_height - stage_height - ball_height:
            ball_val["to_y"] = ball_val["init_spd_y"]    
        else:
            ball_val["to_y"] += 0.5

        ball_val["pos_x"] += ball_val["to_x"]
        ball_val["pos_y"] += ball_val["to_y"]

########################################################################################################
#                                       Collition initialization                                       #
########################################################################################################

    # Set Character rect information
    character_rect = character.get_rect()
    character_rect.left = character_x_pos
    character_rect.top = character_y_pos

    ############################################### Ball ###############################################
    for ball_idx, ball_val in enumerate(balls):
        ball_pos_x = ball_val["pos_x"]
        ball_pos_y = ball_val["pos_y"]
        ball_img_idx = ball_val["img_idx"]

        # Set Ball rect information
        ball_rect = ball_images[ball_img_idx].get_rect()
        ball_rect.left = ball_pos_x
        ball_rect.top = ball_pos_y

        # Ball and character Collition
        if character_rect.colliderect(ball_rect):
            print("Character and ball crashed")
            running = False
            break

        ############################################ Weapon ############################################
        for weapon_idx, weapon_val in enumerate(weapons):
            weapon_pos_x = weapon_val[0]
            weapon_pos_y = weapon_val[1]

            # Set Weapon rect information
            weapon_rect = weapon.get_rect()
            weapon_rect.left = weapon_pos_x
            weapon_rect.top = weapon_pos_y

            # Ball and weapons Collition
            if weapon_rect.colliderect(ball_rect):
                weapon_to_remove = weapon_idx
                ball_to_remove = ball_idx

                # If it is not the smallest ball, division to the next ball
                if ball_img_idx < 3:

                    # Get information about the current ball size
                    ball_width = ball_rect.size[0]
                    ball_height = ball_rect.size[1]

                    # Divided ball information
                    small_ball_rect = ball_images[ball_img_idx + 1].get_rect()
                    small_ball_width = small_ball_rect.size[0]
                    small_ball_height = small_ball_rect.size[1]

                    # A small ball bouncing to the left.
                    balls.append({
                        "pos_x" : ball_pos_x + (ball_width / 2) - (small_ball_width / 2),       # x coordinate of a ball
                        "pos_y" : ball_pos_y + (ball_height / 2) - (small_ball_height / 2),
                        "img_idx" : ball_img_idx + 1,      # index of a ball
                        "to_x" : -3,         # direction of x-axis movement
                        "to_y" : -6,        # if the number is -3, it will move to left.
                        "init_spd_y" : ball_speed_y[ball_img_idx + 1]      # Fist speed
                    })

                    # A small ball bouncing to the right.
                    balls.append({
                        "pos_x" : ball_pos_x + (ball_width / 2) - (small_ball_width / 2),       # x coordinate of a ball
                        "pos_y" : ball_pos_y + (ball_height / 2) - (small_ball_height / 2),
                        "img_idx" : ball_img_idx + 1,      # index of a ball
                        "to_x" : 3,         # direction of x-axis movement
                        "to_y" : -6,        # if the number is -3, it will move to left.
                        "init_spd_y" : ball_speed_y[ball_img_idx + 1]      # Fist speed
                    })

                break
        else: 
            continue
        break
    
    # Remove ball or weapon
    if ball_to_remove > -1:
        del balls[ball_to_remove]
        ball_to_remove = -1

    if weapon_to_remove > -1:
        del weapons[weapon_to_remove]
        weapon_to_remove = -1

    # If there is any ball, Game Over 
    if len(balls) == 0:
        game_result = "Mission Complete"
        running = False

########################################################################################################
#                                               Show to screen                                         #
########################################################################################################

    screen.blit(background, (0,0))      # Make the background

    for weapon_x_pos, weapon_y_pos in weapons:
        screen.blit(weapon, (weapon_x_pos, weapon_y_pos))

    for idx, val in enumerate(balls):
        ball_pos_x = val["pos_x"]
        ball_pos_y = val["pos_y"]
        ball_img_idx = val["img_idx"]
        screen.blit(ball_images[ball_img_idx], (ball_pos_x, ball_pos_y))

    screen.blit(stage, (0, screen_height - stage_height))  # Make the Character on the screen
    screen.blit(character, (character_x_pos, character_y_pos))  # Make the Character on the screen

    # elapsed time calculation
    elapsed_time = (pygame.time.get_ticks() - start_ticks) / 1000   
    # Elapsed time (ms) is expressed in seconds (s) by dividing 1000

    timer = game_font.render("Time : {}".format(int(total_time - elapsed_time)), True, (255, 255, 255))
    # text to print, True, Font colur
    screen.blit(timer, (10, 10))

    # If the time is zero, the game is over.
    if total_time - elapsed_time <= 0:
        game_result = "Time Over"
        running = False

########################################################################################################
#                                       Basic Statements (Must do it)                                  #
########################################################################################################

    pygame.display.update()             # Make the game background again (Each frame)

# Game Over message
msg = game_font.render(game_result, True, (255, 255, 0))    # Yellow
msg_rect = msg.get_rect(center=(int(screen_width / 2, ), int(screen_height / 2)))
screen.blit(msg, msg_rect)

pygame.display.update()  

# a moment's wait
pygame.time.delay(1000)                 # 1 second

# Exit Pygame 
pygame.quit()
