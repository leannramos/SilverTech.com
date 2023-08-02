// Current openings arrows shrink to fit
export const shrinkPositions = () => {
    if (!ResizeObserver)
        return;

    document.querySelectorAll('.current-openings .positions')
        .forEach(positions => {
            const resizeFunctions =
                Array.from(positions.querySelectorAll("a"))
                    .map(a => {
                        const range = document.createRange();
                        range.selectNodeContents(a);

                        return () => {
                            a.style.removeProperty('width');
                            const width = range.getBoundingClientRect().width + 12;
                            a.style.setProperty('width', `${width}px`);
                        };
                    });

            new ResizeObserver(() => {
                resizeFunctions.forEach(resize => {
                    resize();
                });
            }).observe(positions);
        });
}
