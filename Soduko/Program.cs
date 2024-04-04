
using static nk.Runners;
using static nk.GoalsModule;
using static nk.Goals.EqoModule;
using static nk.Logging.LoggerModule;
using static Soduko.Utils.OnceoModule;
using static Soduko.Utils.BoardExt;


using Microsoft.Extensions.Logging;
using Soduko.Utils;

Console.OutputEncoding = System.Text.Encoding.UTF8;
//SetLogLevel(LogLevel.Debug);
SetLogLevel(LogLevel.Information);

#if false
RunAll(1, (q, x) => {
    var board = new object[][]
    {
        Row( x[0] )
    };

    return Conj(Equal(q, board));
}).Render();
#endif 

#if false
RunAll(4, (q, x) => {
    var board = new object[][]
    {
        Row( x[0], x[1] ),
        Row( x[2], x[3] )
    };

    return Conj
    (
        Equal(q, board),
        Onceo(x[0], board.Row(0), board.Col(0)),
        Onceo(x[1], board.Row(0), board.Col(1)),
        Onceo(x[2], board.Row(1), board.Col(0)),
        Onceo(x[3], board.Row(1), board.Col(1))
    );
}).Render();
#endif

#if false
RunAll(9, (q, x) => {
    var board = new object[][]
    {
        Row( x[0], x[1], x[2] ),
        Row( x[3], x[4], x[5] ),
        Row( x[6], x[7], x[8] )
    };

    return Conj
    (
        Equal(q, board),
        Onceo(x[0], board.Row(0), board.Col(0)),
        Onceo(x[1], board.Row(0), board.Col(1)),
        Onceo(x[2], board.Row(0), board.Col(2)),

        Onceo(x[3], board.Row(1), board.Col(0)),
        Onceo(x[4], board.Row(1), board.Col(1)),
        Onceo(x[5], board.Row(1), board.Col(2)),

        Onceo(x[6], board.Row(2), board.Col(0)),
        Onceo(x[7], board.Row(2), board.Col(1)),
        Onceo(x[8], board.Row(2), board.Col(2))
    );
}).Render();
#endif

#if false
RunAll(16, (q, x) => {
    var board = new object[][]
    {
        Row( x[0], x[1], x[2], x[3] ),
        Row( x[4], x[5], x[6], x[7] ),
        Row( x[8], x[9], x[10], x[11] ),
        Row( x[12], x[13], x[14], x[15] )
    };

    return Conj
    (
        Equal(q, board),
        Onceo(x[0], board.Row(0), board.Col(0), board.Box4(0)),
        Onceo(x[1], board.Row(0), board.Col(1), board.Box4(0)),
        Onceo(x[2], board.Row(0), board.Col(2), board.Box4(1)),
        Onceo(x[3], board.Row(0), board.Col(3), board.Box4(1)),

        Onceo(x[4], board.Row(1), board.Col(0), board.Box4(0)),
        Onceo(x[5], board.Row(1), board.Col(1), board.Box4(0)),
        Onceo(x[6], board.Row(1), board.Col(2), board.Box4(1)),
        Onceo(x[7], board.Row(1), board.Col(3), board.Box4(1)),

        Onceo(x[8], board.Row(2), board.Col(0), board.Box4(2)),
        Onceo(x[9], board.Row(2), board.Col(1), board.Box4(2)),
        Onceo(x[10], board.Row(2), board.Col(2), board.Box4(3)),
        Onceo(x[11], board.Row(2), board.Col(3), board.Box4(3)),

        Onceo(x[12], board.Row(3), board.Col(0), board.Box4(2)),
        Onceo(x[13], board.Row(3), board.Col(1), board.Box4(2)),
        Onceo(x[14], board.Row(3), board.Col(2), board.Box4(3)),
        Onceo(x[15], board.Row(3), board.Col(3), board.Box4(3))
    );
}).Render();
#endif

#if true
RunAll(81, (q, x) => {
    var board = new object[][]
    {
        Row(  x[0],  x[1],  x[2],  x[3],  x[4],  x[5],  x[6],  x[7],  x[8]),
        Row(  x[9], x[10], x[11], x[12], x[13], x[14], x[15], x[16], x[17]),
        Row( x[18], x[19], x[20], x[21], x[22], x[23], x[24], x[25], x[26]),

        Row( x[27], x[28], x[29], x[30], x[31], x[32], x[33], x[34], x[35]),
        Row( x[36], x[37], x[38], x[39], x[40], x[41], x[42], x[43], x[44]),
        Row( x[45], x[46], x[47], x[48], x[49], x[50], x[51], x[52], x[53]),

        Row( x[54], x[55], x[56], x[57], x[58], x[59], x[60], x[61], x[62]),
        Row( x[63], x[64], x[65], x[66], x[67], x[68], x[69], x[70], x[71]),
        Row( x[72], x[73], x[74], x[75], x[76], x[77], x[78], x[79], x[80]),
    };

    return Conj
    (
        Equal(q, board),

        Onceo(x[0], board.Row(0), board.Col(0), board.Box9(0)),
        Onceo(x[1], board.Row(0), board.Col(1), board.Box9(0)),
        Onceo(x[2], board.Row(0), board.Col(2), board.Box9(0)),
        Onceo(x[3], board.Row(0), board.Col(3), board.Box9(1)),
        Onceo(x[4], board.Row(0), board.Col(4), board.Box9(1)),
        Onceo(x[5], board.Row(0), board.Col(5), board.Box9(1)),
        Onceo(x[6], board.Row(0), board.Col(6), board.Box9(2)),
        Onceo(x[7], board.Row(0), board.Col(7), board.Box9(2)),
        Onceo(x[8], board.Row(0), board.Col(8), board.Box9(2)),

         Onceo(x[9], board.Row(1), board.Col(0), board.Box9(0)),
        Onceo(x[10], board.Row(1), board.Col(1), board.Box9(0)),
        Onceo(x[11], board.Row(1), board.Col(2), board.Box9(0)),
        Onceo(x[12], board.Row(1), board.Col(3), board.Box9(1)),
        Onceo(x[13], board.Row(1), board.Col(4), board.Box9(1)),
        Onceo(x[14], board.Row(1), board.Col(5), board.Box9(1)),
        Onceo(x[15], board.Row(1), board.Col(6), board.Box9(2)),
        Onceo(x[16], board.Row(1), board.Col(7), board.Box9(2)),
        Onceo(x[17], board.Row(1), board.Col(8), board.Box9(2)),

        Onceo(x[18], board.Row(2), board.Col(0), board.Box9(0)),
        Onceo(x[19], board.Row(2), board.Col(1), board.Box9(0)),
        Onceo(x[20], board.Row(2), board.Col(2), board.Box9(0)),
        Onceo(x[21], board.Row(2), board.Col(3), board.Box9(1)),
        Onceo(x[22], board.Row(2), board.Col(4), board.Box9(1)),
        Onceo(x[23], board.Row(2), board.Col(5), board.Box9(1)),
        Onceo(x[24], board.Row(2), board.Col(6), board.Box9(2)),
        Onceo(x[25], board.Row(2), board.Col(7), board.Box9(2)),
        Onceo(x[26], board.Row(2), board.Col(8), board.Box9(2)),

        Onceo(x[27], board.Row(3), board.Col(0), board.Box9(3)),
        Onceo(x[28], board.Row(3), board.Col(1), board.Box9(3)),
        Onceo(x[29], board.Row(3), board.Col(2), board.Box9(3)),
        Onceo(x[30], board.Row(3), board.Col(3), board.Box9(4)),
        Onceo(x[31], board.Row(3), board.Col(4), board.Box9(4)),
        Onceo(x[32], board.Row(3), board.Col(5), board.Box9(4)),
        Onceo(x[33], board.Row(3), board.Col(6), board.Box9(5)),
        Onceo(x[34], board.Row(3), board.Col(7), board.Box9(5)),
        Onceo(x[35], board.Row(3), board.Col(8), board.Box9(5)),

        Onceo(x[36], board.Row(4), board.Col(0), board.Box9(3)),
        Onceo(x[37], board.Row(4), board.Col(1), board.Box9(3)),
        Onceo(x[38], board.Row(4), board.Col(2), board.Box9(3)),
        Onceo(x[39], board.Row(4), board.Col(3), board.Box9(4)),
        Onceo(x[40], board.Row(4), board.Col(4), board.Box9(4)),
        Onceo(x[41], board.Row(4), board.Col(5), board.Box9(4)),
        Onceo(x[42], board.Row(4), board.Col(6), board.Box9(5)),
        Onceo(x[43], board.Row(4), board.Col(7), board.Box9(5)),
        Onceo(x[44], board.Row(4), board.Col(8), board.Box9(5)),

        Onceo(x[45], board.Row(5), board.Col(0), board.Box9(3)),
        Onceo(x[46], board.Row(5), board.Col(1), board.Box9(3)),
        Onceo(x[47], board.Row(5), board.Col(2), board.Box9(3)),
        Onceo(x[48], board.Row(5), board.Col(3), board.Box9(4)),
        Onceo(x[49], board.Row(5), board.Col(4), board.Box9(4)),
        Onceo(x[50], board.Row(5), board.Col(5), board.Box9(4)),
        Onceo(x[51], board.Row(5), board.Col(6), board.Box9(5)),
        Onceo(x[52], board.Row(5), board.Col(7), board.Box9(5)),
        Onceo(x[53], board.Row(5), board.Col(8), board.Box9(5)),

        Onceo(x[54], board.Row(6), board.Col(0), board.Box9(6)),
        Onceo(x[55], board.Row(6), board.Col(1), board.Box9(6)),
        Onceo(x[56], board.Row(6), board.Col(2), board.Box9(6)),
        Onceo(x[57], board.Row(6), board.Col(3), board.Box9(7)),
        Onceo(x[58], board.Row(6), board.Col(4), board.Box9(7)),
        Onceo(x[59], board.Row(6), board.Col(5), board.Box9(7)),
        Onceo(x[60], board.Row(6), board.Col(6), board.Box9(8)),
        Onceo(x[61], board.Row(6), board.Col(7), board.Box9(8)),
        Onceo(x[62], board.Row(6), board.Col(8), board.Box9(8)),

        Onceo(x[63], board.Row(7), board.Col(0), board.Box9(6)),
        Onceo(x[64], board.Row(7), board.Col(1), board.Box9(6)),
        Onceo(x[65], board.Row(7), board.Col(2), board.Box9(6)),
        Onceo(x[66], board.Row(7), board.Col(3), board.Box9(7)),
        Onceo(x[67], board.Row(7), board.Col(4), board.Box9(7)),
        Onceo(x[68], board.Row(7), board.Col(5), board.Box9(7)),
        Onceo(x[69], board.Row(7), board.Col(6), board.Box9(8)),
        Onceo(x[70], board.Row(7), board.Col(7), board.Box9(8)),
        Onceo(x[71], board.Row(7), board.Col(8), board.Box9(8)),

        Onceo(x[72], board.Row(8), board.Col(0), board.Box9(6)),
        Onceo(x[73], board.Row(8), board.Col(1), board.Box9(6)),
        Onceo(x[74], board.Row(8), board.Col(2), board.Box9(6)),
        Onceo(x[75], board.Row(8), board.Col(3), board.Box9(7)),
        Onceo(x[76], board.Row(8), board.Col(4), board.Box9(7)),
        Onceo(x[77], board.Row(8), board.Col(5), board.Box9(7)),
        Onceo(x[78], board.Row(8), board.Col(6), board.Box9(8)),
        Onceo(x[79], board.Row(8), board.Col(7), board.Box9(8)),
        Onceo(x[80], board.Row(8), board.Col(8), board.Box9(8)),

        Equal(x[0], 4-1), Equal(x[3], 6-1), Equal(x[4], 9-1), Equal(x[5], 7-1),
        Equal(x[11], 3-1), Equal(x[13], 4-1), Equal(x[14], 8-1), Equal(x[16], 6-1),
        Equal(x[19], 6-1), Equal(x[20], 2-1), Equal(x[23], 3-1), Equal(x[26], 8-1),

        Equal(x[27], 5-1), Equal(x[29], 9-1), Equal(x[30], 3-1), Equal(x[32], 1-1),
        Equal(x[37], 4-1), Equal(x[38], 7-1), Equal(x[39], 8-1), Equal(x[42], 1-1),
        Equal(x[46], 3-1), Equal(x[47], 1-1), Equal(x[50], 6-1), Equal(x[52], 9-1),

        Equal(x[56], 6-1), Equal(x[57], 9-1), Equal(x[58], 8-1), Equal(x[62], 3-1),
        Equal(x[63], 1-1), Equal(x[69], 8-1),
        Equal(x[72], 3-1), Equal(x[74], 4-1), Equal(x[79], 2-1), Equal(x[80], 9-1)
    );
}).Render();
#endif

return 0;

// HELPERS
