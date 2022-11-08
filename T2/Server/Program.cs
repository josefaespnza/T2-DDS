using Server;

int mode = PreGameView.AskGameMode();
Game game = new Game(mode);
game.Play();