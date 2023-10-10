nodes
    # For Staging
    Area "Area1"
        Buffers
            b1, b2, b3, b4
        Constraints
            c1
            c2
            c3
        Out
            c3

    Area "Area2"
        Buffers
            b1, b2, b3, b4
        Constraints
            c1
            c2
            c3
        Entrance
            c3

    # For Mixing
    Buffers
        b1, b2, b3, b4
    Constraints
        c1
        c2
        c3
    ...

network

    Area1 --> Area2

    Area1.b1 --> Area.m2
    b1, b2, b2, b4 --> m2
    b1, b2, b2, b4 --> m2