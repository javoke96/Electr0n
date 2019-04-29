
const uri = "api/City";
let cities = null;

function getCount(data) {
  const el = $("#counter");
  let name = "city";
  if (data) {
    if (data > 1) {
      name = "cities";
    }
    el.text(data + " " + name);
  } else {
    el.text("No " + name);
  }
}

$(document).ready(function() {
  getData();
});

function getData() {
  $.ajax({
    type: "GET",
    url: uri,
    cache: false,
    success: function(data) {
      const tBody = $("#cities");

      $(tBody).empty();

      getCount(data.length);

      $.each(data, function(key, city) {
        const tr = $("<tr></tr>")


            .append($("<td></td>").text(city.id))
            .append($("<td></td>").text(city.name))
            .append($("<td></td>").text(city.prev))
          if (city.status) {
              .append($("<td></td>").append($("<img class="status" src="https://www.colorcombos.com/images/colors/92CD00.png">"))
                  }
          else {
 .append($("<td></td>").append($("<img class="status" src="https://www.colorcombos.com/images/colors/AA0114.png">"))
                  }
            
          .append(
            $("<td></td>").append(
              $("<button>Edit</button>").on("click", function() {
                editCity(city.id);
              })
            )
          )
          .append(
            $("<td></td>").append(
              $("<button>Delete</button>").on("click", function() {
                deleteCity(city.id);
              })
            )
          );

        tr.appendTo(tBody);
      });

      cities = data;
    }
  });
}

function addCity() {
  const item = {
    name: $("#name").val(),
    status: $("#status").val()
  };

  $.ajax({
    type: "POST",
    accepts: "application/json",
    url: uri,
    contentType: "application/json",
    data: JSON.stringify(item),
    error: function(jqXHR, textStatus, errorThrown) {
      alert("Something went wrong!");
    },
    success: function(result) {
      getData();
      $("#name").val("");
    }
  });
}

function deleteCity(id) {
  $.ajax({
    url: uri + "/" + id,
    type: "DELETE",
    success: function(result) {
      getData();
    }
  });
}
