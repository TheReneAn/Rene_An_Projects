import random
import pygame

########################################################################################################
#                                   Basic initialization (Must do it)                                  #
########################################################################################################

pygame.init()       # initialization (Must do it)

# Set Window Size
screen_width = 427
screen_height = 640

screen = pygame.display.set_mode((screen_width, screen_height))

# Set Screen Title
pygame.display.set_caption("Poop Game_Rene's project")    # Game Name

# FPS
clock = pygame.time.Clock()

########################################################################################################
#           User game initialization (Background, Game image, Coordinate, Speed, Font etc)             #
########################################################################################################

# Background
background = pygame.image.load("C:\\Users\\auj08\\Desktop\\Python_workspace\\PangGameProject\\PoopQuiz\\background.png")

# Character
character = pygame.image.load("C:\\Users\\auj08\\Desktop\\Python_workspace\\PangGameProject\\PoopQuiz\\character.png")

character_size = character.get_rect().size          # Get character size
character_width = character_size[0]                 # Size of the character width size
character_height = character_size[1]

character_x_pos = (screen_width / 2) - (character_width / 2)    # Set the character's Location at half of the width size in the screen
character_y_pos = screen_height - character_height  # At the end of the screen

# Move Coordinate
to_x = 0
to_y = 0

# Move Speed
character_speed = 0.6

# Enemy character
enemy = pygame.image.load("C:\\Users\\auj08\Desktop\\Python_workspace\\PangGameProject\\PoopQuiz\\poop.png")

enemy_size = enemy.get_rect().size          # Get character size
enemy_width = enemy_size[0]                 # Size of the character width size
enemy_height = enemy_size[1]

enemy_x_pos = random.randint(0, screen_width - enemy_width)
enemy_y_pos = 0

enemy_speed = 5

# Font definition
game_font = pygame.font.Font(None, 40)      # Created Font object (Font, size)

# Totall time
total_time = 10

# Start time
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
    dt = clock.tick(60)                 # Set Frame number per second on the game screen

    for event in pygame.event.get():    # What kind of event has happened?
        if event.type == pygame.QUIT:   # Has an event happened to close the window?
            running = False             # The game was finished
        
        if event.type == pygame.KEYDOWN:    # Check puch the key? or not
            if event.key == pygame.K_LEFT:
                to_x -= character_speed
            elif event.key == pygame.K_RIGHT:
                to_x += character_speed
            elif event.key == pygame.K_UP:
                to_y -= character_speed
            elif event.key == pygame.K_DOWN:
                to_y += character_speed

        if event.type == pygame.KEYUP:      # If the key up, stop the action
            if event.key == pygame.K_LEFT or event.key == pygame.K_RIGHT:
                to_x = 0
            elif event.key == pygame.K_UP or event.key == pygame.K_DOWN:
                to_y = 0
    
    character_x_pos += to_x * dt
    character_y_pos += to_y * dt

########################################################################################################
#                                       Character Position / boundary                                 #
########################################################################################################  

    # Character screeen boundary
    if character_x_pos < 0:
        character_x_pos = 0
    elif character_x_pos > screen_width - character_width:
        character_x_pos = screen_width - character_width

    # Enemy
    enemy_y_pos += enemy_speed

    if enemy_y_pos > screen_height:
        enemy_y_pos = 0
        enemy_x_pos = random.randint(0, screen_width - enemy_width)

########################################################################################################
#                                       Collition initialization                                       #
########################################################################################################

    # Set Collition system
    character_rect = character.get_rect()
    character_rect.left = character_x_pos
    character_rect.top = character_y_pos

    enemy_rect = enemy.get_rect()
    enemy_rect.left = enemy_x_pos
    enemy_rect.top = enemy_y_pos

    # Chect Collition
    if character_rect.colliderect(enemy_rect):
        print("It crashed")
        running = False

########################################################################################################
#                                               Show to screen                                         #
########################################################################################################

    # Make image on the screen
    screen.blit(background, (0,0))      # Make the background
    screen.blit(character, (character_x_pos, character_y_pos))  # Make the Character on the screen
    screen.blit(enemy, (enemy_x_pos, enemy_y_pos))

    # Timer
    # elapsed time calculation
    elapsed_time = (pygame.time.get_ticks() - start_ticks) / 1000   
    # Elapsed time (ms) is expressed in seconds (s) by dividing 1000

    timer = game_font.render(str(int(total_time - elapsed_time)), True, (255, 255, 255))
    # text to print, True, Font colur
    screen.blit(timer, (10, 10))

    # If the time is zero, the game is over.
    if total_time - elapsed_time <= 0:
        print("Time out")
        running = False

    pygame.display.update()             # Make the game background again (Each frame)

# Game Over message
msg = game_font.render(game_result, True, (255, 255, 0))    # Yellow
msg_rect = msg.get_rect(center=(int(screen_width / 2, ), int(screen_height / 2)))
screen.blit(msg, msg_rect)

pygame.display.update()  

# a moment's wait
pygame.time.delay(2000)                 # 2 second

# Exit Pygame 
pygame.quit()
