using GameStore.Dtos;

namespace GameStore.EndPoints;

public static class GameEndPoints
{

    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
        new (
        1,
        "The Legend of Gaming",
        "Adventure",
        49.99m,
        new DateOnly(2022, 3, 15)),
    new (
        2,
        "Strategic Conquest",
        "Strategy",
        29.99m,
        new DateOnly(2021, 8, 22)),
    new (
        3,
        "Racing Rivals",
        "Racing",
        39.99m,
        new DateOnly(2023, 5, 10)),
    new (
        4,
        "Puzzle Quest",
        "Puzzle",
        19.99m,
        new DateOnly(2020, 12, 5))
    ];


    public static RouteGroupBuilder MapGameEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        // GET games/
        group.MapGet("/", () => games);

        // GET games/game
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });


        // PUT game
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE game
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;

    }
}
