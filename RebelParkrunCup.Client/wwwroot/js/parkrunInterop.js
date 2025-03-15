async function fetchParkrunData(parkrunId) {
    const response = await fetch(`https://www.parkrun.org.uk/parkrunner/${parkrunId}/all/`);
    const text = await response.text();
    return text;
}