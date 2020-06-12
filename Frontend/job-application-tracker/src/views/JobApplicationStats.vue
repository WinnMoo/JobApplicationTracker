<template>
  <v-card class="mx-auto text-center"
    color="green"
    dark>
    <v-card-text>
      <v-sheet color="rgba(0, 0, 0, .12)">
        <v-sparkline
          :fill="fill"
          :gradient="gradient"
          :line-width="width"
          :padding="padding"
          :smooth="radius || false"
          :value="value"
          auto-draw
        >
          <template v-slot:label="item">
            {{ item.value }}
          </template>
        </v-sparkline>
      </v-sheet>
    </v-card-text>

    <v-card-text>
      <div class="display-1 font-weight-thin">Job Applications within the past month</div>
    </v-card-text>
  </v-card>
</template>
<script>
export default {
  name: "JobApplicationStats",
  data() {
    return {
      fill: true,
      padding: 8,
      radius: 10,
      value: [0, 2, 5, 9, 5, 10, 3, 5, 0, 0, 1, 8, 2, 9, 0],
      width: 2,
      gradients: [
        ['#222'],
        ['#42b3f4'],
        ['red', 'orange', 'yellow'],
        ['purple', 'violet'],
        ['#00c6ff', '#F0F', '#FF0'],
        ['#f72047', '#ffd200', '#1feaea'],
      ]
    }
  }
}
</script>
