const withMT = require("@material-tailwind/react/utils/withMT");

module.exports = withMT({
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        rubikmonoone: ['"Rubik Mono One"', 'monospace'],
        poppins: ['Poppins', 'sans-serif'],
        alfa: ['Alfa Slab One', 'serif'],
      },
      backgroundImage: {
        'banner': "url('/assets/images/banner.png')",
        'footer': "url('/assets/images/footer.png')"
      }
    },
  },
  plugins: [],
});