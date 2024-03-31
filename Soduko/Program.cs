
using static nk.Runners;
using static nk.Goals;
using static nk.Logging.LoggerModule;
using static Soduko.Utils.Goals;
using static Soduko.Utils.BoardExt;

using Microsoft.Extensions.Logging;
using Soduko.Utils;

Console.OutputEncoding = System.Text.Encoding.UTF8;
SetLogLevel(LogLevel.Debug);

#if false
RunAll(1, (q, x) => {
    var board = new Board
    (
        Row( 1 )
    );

    return Conj(Eqo(q, board));
}).Render();
#endif 

RunAll(4, (q, x) => {
    var board = new object[][]
    {
        Row( x[0], x[1] ),
        Row( x[2], x[3] )
    };

    return Conj
    (
        Eqo(q, board),
        Onceo(x[0], board.Row(0), board.Col(0)),
        Onceo(x[1], board.Row(0), board.Col(1))
    );
}).Render();

return 0;

// HELPERS
