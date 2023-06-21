(function () {

    /*INIT*/
    var svg = document.getElementsByTagName("svg")[0]
    var body = document.getElementsByTagName("body")[0]
    var g = svg.querySelector("g")
    const minFactor = svg.clientWidth > svg.clientHeight ? svg.clientHeight : svg.clientWidth
    const WIDTH = minFactor > 1200 ? 65 : minFactor > 950 ? 55 : minFactor > 750 ? 45 : 35
    const COLS = Math.floor(svg.clientWidth / WIDTH)
    const ROWS = Math.floor(svg.clientHeight / WIDTH)
    const TOTAL = (COLS + 1) * (ROWS + 1)
    const CENTERX = Math.floor(COLS / 2)
    const CENTERY = Math.floor(ROWS / 2)
    document.getElementById("squareCount").innerHTML = `[${TOTAL}]`

    /*theme config + theme func = theme*/
    var themes = {
        "ArkStarmap": {
            key: "ArkStarmap",
            url: "https://s3-us-west-2.amazonaws.com/s.cdpn.io/1197275/sky4.jpg",
            /*see pen details for image credits*/
            base: "rgba(48, 69, 95, 0.45)",
            solid1: "rgba(48, 69, 95, 0.55)",
            solid2: "rgba(48, 69, 95, 0.65)",
            solid3: "rgba(48, 69, 95, 0.75)",
            time1: 100,
            time2: 200,
            time3: 900,
            func: ArkStarmap
        },
        "Raindrops": {
            key: "Raindrops",
            url: "https://s3-us-west-2.amazonaws.com/s.cdpn.io/1197275/rain.jpg",
            /*see pen details for image credits*/
            base: "rgba(48, 69, 95, 0.45)",
            solid1: "rgba(48, 69, 95, 0.75)",
            func: rainDrops
        },
        "Eaters": {
            key: "Eaters",
            base: "rgba(48, 69, 95, .6)",
            solid1: "rgba(52, 70, 99, .9)",
            solid2: "#000",
            func: eaters
        },
        "Glass": {
            key: "Glass",
            base: "rgba(10,0,0, 0)",
            gutter: 4,
            func: glass,
            className: "glass"
        },
        "Radiator": {
            key: "Radiator",
            base: "#000",
            func: radiator
        },
        "RGBY": {
            key: "RGBY",
            func: rgbyMixer,
            base: "#000",
            className: "RGB"
        },
        "Chasers": {
            key: "Chasers",
            func: chasers,
            base: "#000"
        },
        "Particles": {
            key: "Particles",
            url: "https://s3-us-west-2.amazonaws.com/s.cdpn.io/1197275/milky3.jpg",
            /*see pen details for image credits*/
            func: particles,
            base: "rgba(10,0,0, .3)",
            solid1: "rgba(86, 86, 149, .3)",
            solid2: "rgba(86, 86, 149, .5)",
            className: "particles"
        },
        "Fibonacci": {
            key: "Fibonacci",
            func: fibonacci,
            base: "#000"
        },
        "Orbital": {
            key: "Orbital",
            url: "https://s3-us-west-2.amazonaws.com/s.cdpn.io/1197275/crab.jpg",
            /*see pen details for image credits*/
            func: orbital,
            base: "rgba(10,0,0, 0)",
            solid1: "blue"
        },
        "Maze": {
            key: "Maze",
            func: maze,
            className: "maze"
        }
    }

    const params = new URLSearchParams(window.location.search);
    const theme = themes[params && params.get("preset") || "ArkStarmap"]
    let themeEl = document.getElementById("themes")
    themeEl.value = theme.key
    themeEl.addEventListener("change", changePreset)

    body.onload = () => {
        g.style.fill = theme.base
        if (theme.className) body.className = theme.className
        if (theme.url) body.style.backgroundImage = `url('${theme.url}')`
        buildBoxes(theme.base, theme.gutter)
        theme.func()
    }

    /* PRESETS */
    function ArkStarmap() {

        var arc = themes["ArkStarmap"]
        const BATCHES = 30
        for (var i = 0; i < BATCHES; i++) {
            oneSquare(arc.solid1, arc.time1)
            oneSquare(arc.solid3, arc.time1)
            oneSquare(arc.solid2, arc.time3)
        }
        quadRunner(arc.solid3, arc.time1)
        quadRunner(arc.solid3, arc.time2)
        quadRunner(arc.solid2, arc.time3)

        async function oneSquare(solid, time) {

            let randomPoint = getRandomPoint()
            let target = getTarget(randomPoint.row, randomPoint.col)
            target.setAttribute("fill", solid)
            await delay(time)
            target.setAttribute("fill", arc.base)
            oneSquare(solid, time)
        }
        async function quadRunner(color, time) {

            let randomPoint = getRandomPoint()
            let row = randomPoint.row
            let col = randomPoint.col
            let t1 = getTarget(row, col)
            let t2 = getTarget(row, col + 1)
            let t3 = getTarget(row + 1, col)
            let t4 = getTarget(row + 1, col + 1)
            t1 && t1.setAttribute("fill", color)
            t2 && t2.setAttribute("fill", color)
            t3 && t3.setAttribute("fill", color)
            t4 && t4.setAttribute("fill", color)
            await delay(time)
            t1 && t1.setAttribute("fill", arc.base)
            t2 && t2.setAttribute("fill", arc.base)
            t3 && t3.setAttribute("fill", arc.base)
            t4 && t4.setAttribute("fill", arc.base)
            quadRunner(color, time)
        }
    }

    async function rainDrops() {

        let rain = themes["Raindrops"]
        for (let i = 0; i <= COLS; i++) {
            let time = Math.random() * 60
            colIterator(i, time)
        }
        await delay(2000)
        for (let i = 0; i <= COLS; i++) {
            let time = Math.random() * 60
            colIterator(i, time)
        }

        async function colIterator(start, time) {
            //iterators gonna iterate
            for (var pos = 0; pos <= ROWS; pos++) {
                if (pos == ROWS) {
                    colIterator(start, time)
                }

                let target = getTarget(pos, start)
                target.setAttribute("fill", rain.solid1)
                await delay(time)
                target.setAttribute("fill", rain.base)
            }
        }
    }

    function eaters() {
        let theme = themes["Eaters"]
        for (let i = 0; i < 30; i++) {
            eat(CENTERY, CENTERX, Math.random() * 300)
        }

        async function eat(row, col, time) {

            let target = getTarget(row, col)
            target.setAttribute("fill", theme.solid1)
            await delay(time)
            if (target) target.setAttribute("fill", theme.solid2)

            let next = getRandomMove(new Point(row, col))
            eat(next.row, next.col, time)
        }

    }

    function glass() {

        changeOne("#000", 180)
        changeOne("#000", 180)
        changeOne("#000", 180)
        panelRunner("#000", 180)
        panelRunner("#000", 180)
        panelRunner("#000", 180)

        document.querySelectorAll("rect").forEach(rect => {
            rect.setAttribute("rx", "2px")
            rect.setAttribute("ry", "2px")
        })

        async function changeOne(color, time) {

            let randoPoint = getRandomPoint()
            let target = getTarget(randoPoint.row, randoPoint.col)
            target.setAttribute("fill", color)
            await delay(time)
            changeOne(tinycolor.random().toHexString(), time)
        }
        async function panelRunner(color, time) {

            let randoPoint = getRandomPoint()
            let row = randoPoint.row
            let col = randoPoint.col

            //always at least a little tetris block
            let t1 = getTarget(row, col)
            let t2 = getTarget(row, col + 1)
            let t3 = getTarget(row + 1, col)
            let t4 = getTarget(row + 1, col + 1)
            t1 && t1.setAttribute("fill", color)
            t2 && t2.setAttribute("fill", color)
            t3 && t3.setAttribute("fill", color)
            t4 && t4.setAttribute("fill", color)

            //and maybe bigger
            if (Math.random() > .5) {
                //vertical block
                let t5 = getTarget(row + 2, col + 1)
                let t6 = getTarget(row + 2, col)
                t5 && t5.setAttribute("fill", color)
                t6 && t6.setAttribute("fill", color)

                if (Math.random() > .5) {
                    //maybe large
                    let t7 = getTarget(row + 3, col + 1)
                    let t8 = getTarget(row + 3, col)
                    t7 && t7.setAttribute("fill", color)
                    t8 && t8.setAttribute("fill", color)
                }

            } else {
                //horizontal block
                if (Math.random() > .5) {
                    let right1 = getTarget(row, col + 2)
                    let right2 = getTarget(row + 1, col + 2)
                    right1 && right1.setAttribute("fill", color)
                    right2 && right2.setAttribute("fill", color)
                    //maybe large
                    if (Math.random() > .5) {
                        let right3 = getTarget(row, col + 3)
                        let right4 = getTarget(row + 1, col + 3)
                        right3 && right3.setAttribute("fill", color)
                        right4 && right4.setAttribute("fill", color)
                    }
                }
            }

            await delay(time)
            panelRunner(tinycolor.random().desaturate(10).darken(10).toHexString(), time)
        }

    }

    async function radiator() {

        let theme = themes["Radiator"]
        let origin = new Point(CENTERY, CENTERX)
        const speed = 60
        const interval = 350
        for (var i = 0; i < 4; i++) {
            radiate([origin], speed, "white")
            await delay(interval)
        }

        async function radiate(points, time, color) {

            points.forEach(point => {
                let square = getTarget(point.row, point.col)
                square && square.setAttribute("fill", color)
            })

            await delay(time)
            points.forEach(point => {
                let square = getTarget(point.row, point.col)
                square && square.setAttribute("fill", theme.base)
            })

            if (points.length > 1 && points.filter(p => p.col < 0 || p.row < 0 || p.col > COLS || p.row > ROWS).length == points.length) {
                radiate([origin], time, tinycolor.random().toHexString())
            } else {
                let thesePoints = getSurrounding(points)
                radiate(thesePoints, time, color)
            }
        }

        function getSurrounding(points) {
            let newPoints = []
            if (points.length == 1) {
                let p = points[0]
                newPoints.push(new Point(p.row + 1, p.col, "bottom"))
                newPoints.push(new Point(p.row - 1, p.col, "top"))
                newPoints.push(new Point(p.row, p.col + 1, "right"))
                newPoints.push(new Point(p.row, p.col - 1, "left"))
                newPoints.push(new Point(p.row + 1, p.col + 1, "end-bottomRight"))
                newPoints.push(new Point(p.row + 1, p.col - 1, "end-bottomLeft"))
                newPoints.push(new Point(p.row - 1, p.col - 1, "end-topLeft"))
                newPoints.push(new Point(p.row - 1, p.col + 1, "end-topRight"))
            } else {
                points.forEach((p, index, points) => {

                    switch (p.type) {
                        case "end-bottomRight":
                            newPoints.push(new Point(p.row, p.col + 1, "right")) //right
                            newPoints.push(new Point(p.row + 1, p.col, "bottom")) //down
                            newPoints.push(new Point(p.row + 1, p.col + 1, "end-bottomRight")) //end
                            break
                        case "end-bottomLeft":
                            newPoints.push(new Point(p.row, p.col - 1, "left")) //left
                            newPoints.push(new Point(p.row + 1, p.col, "bottom")) //down
                            newPoints.push(new Point(p.row + 1, p.col - 1, "end-bottomLeft")) //end
                            break
                        case "end-topRight":
                            newPoints.push(new Point(p.row, p.col + 1, "right")) //right
                            newPoints.push(new Point(p.row - 1, p.col, "top")) //up
                            newPoints.push(new Point(p.row - 1, p.col + 1, "end-topRight")) //end
                            break
                        case "end-topLeft":
                            newPoints.push(new Point(p.row, p.col - 1, "left")) //left
                            newPoints.push(new Point(p.row - 1, p.col, "top")) //up
                            newPoints.push(new Point(p.row - 1, p.col - 1, "end-topLeft")) //end
                            break
                        case "right":
                            newPoints.push(new Point(p.row, p.col + 1, "right"))
                            break
                        case "left":
                            newPoints.push(new Point(p.row, p.col - 1, "left"))
                            break
                        case "top":
                            newPoints.push(new Point(p.row - 1, p.col, "top"))
                            break
                        case "bottom":
                            newPoints.push(new Point(p.row + 1, p.col, "bottom"))
                            break
                    }
                })
            }
            return newPoints
        }

    }

    function rgbyMixer() {

        mover(0, 0, 10, "red")
        mover(ROWS, COLS, 10, "blue")
        mover(0, COLS, 10, "yellow")
        mover(ROWS, 0, 10, "green")

        async function mover(row, col, time, className) {

            let target = getTarget(row, col)
            target.setAttribute("class", className)

            let nextMove = getRandomMove(new Point(row, col))
            await delay(time)
            mover(nextMove.row, nextMove.col, time, className)
        }
    }

    function chasers() {
        let theme = themes["Chasers"]
        svg.setAttribute("class", "chasers")

        for (var i = 0; i < 100; i++) {
            chase(new Point(0, 0), Math.floor(Math.random() * 400), tinycolor.random().toHexString())
        }

        async function chase(start, time, color) {

            let target = getTarget(start.row, start.col)
            target.setAttribute("fill", color)
            await delay(time)
            target.setAttribute("fill", theme.base)

            let nextPoint = getNextPoint(start)
            if (nextPoint) {
                chase(nextPoint, time, color)
            }

        }
    }

    function particles() {
        let theme = themes["Particles"]
        let bounceMap = {
            "right": {
                "northEast": "northWest",
                "southEast": "southWest",
            },
            "down": {
                "southEast": "northEast",
                "southWest": "northWest",
            },
            "left": {
                "southWest": "southEast",
                "northWest": "northEast"
            },
            "up": {
                "northEast": "southEast",
                "northWest": "southWest",
            }
        }
        let reversalMap = {
            "northEast": "southWest",
            "southEast": "northWest",
            "northWest": "southEast",
            "southWest": "northEast"
        }

        let min = 20 /*time range*/
        let max = 150
        for (let i = 0; i < 15; i++) {
            let seed = Math.random()
            let seed2 = Math.random()
            let randCol = parseInt(seed * COLS)
            let randRow = parseInt(seed2 * ROWS)
            let time = seed2 * (max - min) + min
            let direction = seed > .75 ? "southWest" : seed > .5 ? "southEast" : seed > .25 ? "northEast" : "northWest"
            randCol = randCol == 0 ? 1 : randCol //move points in from perimeter, causes issues when starting on outside.
            randRow = randRow == 0 ? 1 : randRow
            let p = new Point(randRow, randCol)
            moveDiagonally(p, time, seed > .5 ? theme.solid1 : theme.solid2, theme.base, direction)
        }

        async function moveDiagonally(point, time, color, base, direction) {

            let target = getTarget(point.row, point.col)
            target.setAttribute("fill", color)
            await delay(time)
            target.setAttribute("fill", base)

            if (isBoundary(target)) {
                if (isCorner(target)) {
                    direction = reversalMap[direction]
                } else { //against the wall
                    let side = whichBoundary(target)
                    direction = bounceMap[side][direction]
                }
            }
            let nextPoint = getNextPointInDirection(point, direction)
            moveDiagonally(nextPoint, time, color, base, direction)
        }
    }

    async function fibonacci() {

        var remaining = (ROWS + 1) * (COLS + 1)
        let time = 200
        let target = getTarget(0, 0)
        target.setAttribute("fill", tinycolor.random().toHexString())
        await delay(time)
        target = getTarget(0, 1)
        target.setAttribute("fill", tinycolor.random().toHexString())
        await delay(time)
        remaining = remaining - 2
        goFibonacci(new Point(0, 2), 1, 1)

        async function goFibonacci(start, term1, term2) {
            let sum = term1 + term2
            time = time - (10 + term2)
            let nextPoint = await paint(start, sum, time, tinycolor.random().toHexString())
            remaining = remaining - sum
            if (remaining >= term2 + sum) {
                goFibonacci(nextPoint, term2, sum)
            } else {
                for (var i = 0; i < remaining; i++) {
                    let target = getTarget(nextPoint.row, nextPoint.col)
                    if (target) {
                        target.setAttribute("fill", "rgba(255,255,255,.2)") //grayed out
                        target.setAttribute("rx", "50%")
                        target.setAttribute("ry", "50%")
                    }
                    nextPoint = getNextPoint(nextPoint)
                }
            }
        }

        async function paint(point, howMany, time, color) {
            let nextPointToProcess = new Point(point.row, point.col)
            for (var i = 0; i < howMany; i++) {
                let target = getTarget(nextPointToProcess.row, nextPointToProcess.col)
                target && target.setAttribute("fill", color)
                nextPointToProcess = getNextPoint(nextPointToProcess)
                await delay(time)
            }
            return nextPointToProcess
        }
    }

    function orbital() {

        let theme = themes["Orbital"]
        let numRunners = (COLS > ROWS ? ROWS / 2 : COLS / 2)
        let baseTime = 200
        let timeReduction = 20
        let darkenReduction = 5

        for (var i = 0; i < numRunners; i++) {
            let time = baseTime - timeReduction * i
            let darken = time / darkenReduction
            let color = tinycolor.random().darken(darken).toHexString()
            runTrack(i, "CW", time, color)
        }
        for (var i = 0; i < numRunners; i++) {
            let time = baseTime - timeReduction * i
            let darken = time / darkenReduction
            let color = tinycolor.random().darken(darken).toHexString()
            runTrack(i, "CCW", time, color)
        }
        async function runTrack(index, direction, time, color) {

            let columnMoves = COLS - 2 * index
            let rowMoves = ROWS - 2 * index
            let start = new Point(index, index)
            let topRight = new Point(start.row, start.col + columnMoves)
            let bottomRight = new Point(start.row + rowMoves, start.col + columnMoves)
            let bottomLeft = new Point(start.row + rowMoves, start.col)

            if (direction == "CW") {
                await rowIterator(start, columnMoves, time, "right", color)
                await colIterator(topRight, rowMoves, time, "down", color)
                await rowIterator(bottomRight, columnMoves, time, "left", color)
                await colIterator(bottomLeft, rowMoves, time, "up", color)
            } else {
                await colIterator(bottomRight, rowMoves, time, "up", color)
                await rowIterator(topRight, columnMoves, time, "left", color)
                await colIterator(start, rowMoves, time, "down", color)
                await rowIterator(bottomLeft, columnMoves, time, "right", color)
            }

            runTrack(index, direction, time, color)

        }
        async function rowIterator(start, howMany, time, direction, color) {
            for (var i = 0; i < howMany; i++) {
                let target = getTarget(start.row, start.col)
                target.setAttribute("fill", color)
                start.col = direction == "right" ? start.col + 1 : start.col - 1
                await delay(time)
                target.setAttribute("fill", theme.base)
            }
        }
        async function colIterator(start, howMany, time, direction, color) {
            for (var i = 0; i < howMany; i++) {
                let target = getTarget(start.row, start.col)
                target.setAttribute("fill", color)
                start.row = direction == "down" ? start.row + 1 : start.row - 1
                await delay(time)
                target.setAttribute("fill", theme.base)
            }
        }
    }

    async function maze() {

        let blockFactor = .23
        let time = TOTAL > 500 ? 30 : TOTAL > 250 ? 40 : TOTAL > 150 ? 50 : 60

        mazer(0, 0, time)
        mazer(ROWS, COLS, time)
        mazer(0, COLS, time)
        if (TOTAL > 100) mazer(ROWS, 0, time)
        if (TOTAL > 500) {
            mazer(CENTERY, CENTERX, time)
        }

        await delay(1500)
        let rp1 = getRandomPoint()
        lostSquare(getTarget(rp1.row, rp1.col), TOTAL > 1000 ? 80 : 100, getRandomDirection(), "solid1")
        let rp2 = getRandomPoint()
        lostSquare(getTarget(rp2.row, rp2.col), TOTAL > 1000 ? 55 : 75, getRandomDirection(), "solid2")
        let rp3 = getRandomPoint()
        lostSquare(getTarget(rp3.row, rp3.col), TOTAL > 1000 ? 25 : 35, getRandomDirection(), "solid3")
        let rp4 = getRandomPoint()
        lostSquare(getTarget(rp4.row, rp4.col), TOTAL > 1000 ? 10 : 25, getRandomDirection(), "solid4")

        async function mazer(row, col, time) {

            let rando = Math.random()
            let target = getTarget(row, col)
            target.setAttribute("class", `${rando < blockFactor ? "blocker" : "base"}`)
            let next = getRandomMove(new Point(row, col), rando)

            await delay(time)
            mazer(next.row, next.col, time)
        }

        async function lostSquare(target, time, direction, className) {

            target.setAttribute("class", className)
            let nextPoint = getNextPointInDirection(new Point(target.getAttribute("row"), target.getAttribute("col")), direction)
            let nextTarget = getTarget(nextPoint.row, nextPoint.col)

            await delay(time)
            target.setAttribute("class", "base")

            if (!nextTarget || nextTarget.classList.contains("blocker")) {
                let newDirection = getRandomDirection(direction)
                lostSquare(target, time, newDirection, className)
            } else {
                //no loops
                if (Math.random() > .95) {
                    lostSquare(nextTarget, time, getRandomDirection(direction), className)
                } else {
                    lostSquare(nextTarget, time, direction, className)
                }
            }
        }

    }

    /* helpers */
    function buildBoxes(color, gutter) {
        gutter = gutter === undefined ? 1 : gutter
        for (var col = 0; col <= COLS; col++) {
            for (var row = 0; row <= ROWS; row++) {
                let x = WIDTH * col
                let y = WIDTH * row
                drawSquare(row, col, x, y, WIDTH - gutter, WIDTH - gutter, color)
            }
        }
    }

    function Point(row, col, type) {
        this.col = parseInt(col)
        this.row = parseInt(row)
        this.type = type
    }

    function getNextPoint(point) {
        let isEndOfRow = point.col == COLS
        let newRow = isEndOfRow ? point.row + 1 : point.row
        let newCol = isEndOfRow ? 0 : point.col + 1
        if (newRow > ROWS) return undefined
        return new Point(newRow, newCol)
    }

    function getNextPointInDirection(point, direction) {
        let row = point.row
        let col = point.col
        switch (direction) {
            case "north":
                return new Point(row - 1, col)
                break
            case "south":
                return new Point(row + 1, col)
                break
            case "east":
                return new Point(row, col + 1)
                break
            case "west":
                return new Point(row, col - 1)
                break
            case "northEast":
                return new Point(row - 1, col + 1)
                break
            case "southEast":
                return new Point(row + 1, col + 1)
                break
            case "northWest":
                return new Point(row - 1, col - 1)
                break
            case "southWest":
                return new Point(row + 1, col - 1)
                break
        }
    }

    function getRandomMove(from, xRando = Math.random(), yRando = Math.random()) {

        var xMove = xRando > .66 ? 1 : xRando > .33 ? 0 : -1
        var yMove = yRando > .66 ? 1 : yRando > .33 ? 0 : -1

        if (from.row + yMove > ROWS) yMove = 0
        if (from.row + yMove < 0) yMove = 0
        if (from.col + xMove < 0) xMove = 0
        if (from.col + xMove > COLS) xMove = 0

        return new Point(from.row + yMove, from.col + xMove)
    }

    function getRandomPoint() {
        let row = Math.floor(Math.random() * (ROWS + 1))
        let col = Math.floor(Math.random() * (COLS + 1))
        return new Point(row, col)
    }

    function getRandomDirection(not) {

        var generate = () => {
            let seed = Math.random()
            return seed > .75 ? "south" : seed > .5 ? "north" : seed > .25 ? "east" : "west"
        }
        let which = generate()
        while (not && which == not) {
            which = generate()
        }
        return which
    }

    function getTarget(row, col) {
        return document.querySelector(`rect[col='${col}'][row='${row}']`)
    }

    function isBoundary(el) {
        let row = el.getAttribute("row")
        let col = el.getAttribute("col")
        return row == 0 || row == ROWS || col == 0 || col == COLS
    }

    function whichBoundary(el) {
        let row = el.getAttribute("row")
        let col = el.getAttribute("col")
        return row == 0 ? "up" : row == ROWS ? "down" : col == 0 ? "left" : col == COLS ? "right" : undefined
    }

    function isCorner(el) {
        let row = el.getAttribute("row")
        let col = el.getAttribute("col")
        return (row == 0 && col == 0) ||
            (col == 0 && row == ROWS) ||
            (col == COLS && row == ROWS) ||
            (row == 0 && col == COLS)
    }

    function changePreset(e) {
        document.location.search = `preset=${e.target.value}`
    }

    function delay(ms) {
        return new Promise(done => setTimeout(() => {
            done()
        }, ms))
    }

    function drawSquare(row, col, x, y, w, h, color) {
        const rect = document.createElementNS("http://www.w3.org/2000/svg", "rect")
        rect.setAttribute("x", x)
        rect.setAttribute("y", y)
        rect.setAttribute("row", row)
        rect.setAttribute("col", col)
        rect.setAttribute("width", w)
        rect.setAttribute("height", h)
        g.appendChild(rect)

    }
})("Visit me at sweaverD.com!")