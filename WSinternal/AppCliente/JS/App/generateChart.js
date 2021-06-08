var data = [12, 19, 3, 5];
var labels = ['Estandar', 'Deluxe', 'Ultra Deluxe', 'Presidencial'];

var ctx = document.getElementById('myChart').getContext('2d');

var myChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: labels,
        datasets: [{
            label: 'Categorias mas Reservadas',
            data: data,
            borderWidth: 0,
        }],
    },
    options: {
        legend: {
            labels: {
                fontColor: "#f7fafc",
            }
        },
    }
});