import { useCallback, useState } from "react";
// import Breadcrumb from "../components/Breadcrumb";
import Pagination from "../components/Product/Pagination";
import ProductCard from "../components/Product/ProductCard";
// import Slider from "./Slider";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import PriceRangeSlider from "../components/Product/PriceRangeSlider ";

function ProductPage() {
  const categories = [
    { name: "New in Closet (50)", value: "new-in-closet" },
    { name: "Featured Items (150)", value: "featured-items" },
    { name: "Men's Wear (220)", value: "mens-wear" },
    { name: "Women's Wear (350)", value: "womens-wear" },
    { name: "Kids' Wear (120)", value: "kids-wear" },
    { name: "Sports Shoes (80)", value: "sports-shoes" },
    { name: "Sports Equipment (70)", value: "sports-equipment" },
  ];

  const brands = [
    { name: "Nike (430)", value: "nike" },
    { name: "Puma (320)", value: "puma" },
    { name: "Adidas (300)", value: "adidas" },
    { name: "Reebok (280)", value: "reebok" },
    { name: "New Balance (150)", value: "new-balance" },
    { name: "Skechers (100)", value: "skechers" },
    { name: "Others (80)", value: "others" },
  ];

  const products = Array.from({ length: 50 }, (_, index) => ({
    category: "SHOES",
    name: `Product ${index + 1}`,
    price: parseFloat((Math.random() * 100).toFixed(2)),
    image: "https://via.placeholder.com/300",
  }));

  const [sortBy, setSortBy] = useState("popularity");
  const [currentPage, setCurrentPage] = useState(1);

  const handleSortChange = useCallback((event) => {
    setSortBy(event.target.value);
  }, []);

  const itemsPerPage = 15;
  const currentItems = products.slice(
    (currentPage - 1) * itemsPerPage,
    currentPage * itemsPerPage
  );

  const handleClearFilters = () => {
    console.log("All filters cleared");
  };

  return (
    <div className="pt-28 py-10">
      <div className="container mx-auto pt-4 px-20">
        {/* <Breadcrumb firstli="Home" secondli="Products" /> */}

        <div className="grid grid-cols-1 lg:grid-cols-4 gap-4 mt-2">
          <div className="w-full lg:col-span-1">
            <div className="ProductWrapper w-full">
              <div className="ProductTitle mb-4 font-bold text-3xl">
                Products
              </div>

              <div className="Products text-black font-bold">Categories</div>
              <div className="Categories relative p-4">
                <div className="grid grid-cols-1 gap-2">
                  {categories.map((category, index) => (
                    <label key={index} className="inline-flex items-center">
                      <input
                        type="checkbox"
                        className="form-checkbox h-5 w-5 text-orange-500"
                        value={category.value}
                      />
                      <span className="ml-2 text-black">{category.name}</span>
                    </label>
                  ))}
                </div>
              </div>

              <div className="h-px bg-gray-300 my-5 mx-auto"></div>

              <div className="Products text-black font-bold">Brands</div>
              <div className="Categories relative p-4">
                <div className="grid grid-cols-1 gap-2">
                  {brands.map((brand, index) => (
                    <label key={index} className="inline-flex items-center">
                      <input
                        type="checkbox"
                        className="form-checkbox h-5 w-5 text-orange-500"
                        value={brand.value}
                      />
                      <span className="ml-2 text-black">{brand.name}</span>
                    </label>
                  ))}
                </div>
              </div>

              <div className="h-px bg-gray-300 my-5 mx-auto"></div>

              <div className="Products text-black font-bold">Size</div>

              <div className="Sizes relative p-4">
                <div className="grid grid-cols-4 gap-2">
                  {["XS", "S", "M", "L", "XL", "2XL"].map((size) => (
                    <button
                      key={size}
                      className="bg-transparent border border-black text-black font-bold py-2 px-4 rounded"
                    >
                      {size}
                    </button>
                  ))}
                </div>
              </div>
              <div className="h-px bg-gray-300 my-5 mx-auto"></div>
              <div>
                <div className="Products text-black font-bold">Price</div>
                <PriceRangeSlider />
                <div className="h-px bg-gray-300 my-5 mx-auto"></div>

                <div className="flex items-center justify-center mt-4 w-fit">
                  <button
                    onClick={handleClearFilters}
                    className="flex items-center text-black font-bold underline"
                  >
                    <FontAwesomeIcon icon={faXmark} />
                    CLEAR ALL FILTER
                  </button>
                </div>
              </div>

              <div className="relative inline-block mt-6">
                <img src="/assets/images/product/hero.png" alt="Hero" />
                <div className="absolute top-[10%] left-1/2 transform -translate-x-1/2 text-center p-[10px]">
                  <span className="text-white font-bold text-xl font-poppins">
                    Get Yours
                  </span>
                  <br />
                  <span className="text-white font-bold text-3xl uppercase font-poppins">
                    Best Gear
                  </span>
                  <br />
                </div>

                <div className="absolute top-[85%] left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-orange-500 p-2">
                  <span className="text-black font-bold">Shop Now</span>
                </div>
              </div>
            </div>
          </div>
          <div className="w-full lg:col-span-3">
            <div className="py-6">
              <div className="flex justify-between items-center border-b pb-4 mb-4">
                <div className="text-sm text-gray-600">
                  Showing {(currentPage - 1) * itemsPerPage + 1} -{" "}
                  {Math.min(currentPage * itemsPerPage, products.length)} of{" "}
                  {products.length} results
                </div>
                <div className="flex items-center">
                  <span className="mr-2 text-sm text-gray-600">Sort by</span>
                  <select
                    className="block pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"
                    value={sortBy}
                    onChange={handleSortChange}
                  >
                    <option value="popularity">Popularity</option>
                    <option value="price">Price</option>
                    <option value="rating">Rating</option>
                  </select>
                  <div className="ml-4 flex items-center space-x-2">
                    <button>
                      <svg
                        className="w-5 h-5 text-orange-500"
                        fill="none"
                        stroke="currentColor"
                        viewBox="0 0 24 24"
                      >
                        <path
                          strokeLinecap="round"
                          strokeLinejoin="round"
                          strokeWidth="2"
                          d="M3 12h18m-6 6h6m-6-6h6m-6-6h6"
                        />
                      </svg>
                    </button>
                    <button>
                      <svg
                        className="w-5 h-5 text-gray-400"
                        fill="none"
                        stroke="currentColor"
                        viewBox="0 0 24 24"
                      >
                        <path
                          strokeLinecap="round"
                          strokeLinejoin="round"
                          strokeWidth="2"
                          d="M4 6h16M4 10h16M4 14h16M4 18h16"
                        />
                      </svg>
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
              {currentItems.map((product, index) => (
                <ProductCard key={index} product={product} />
              ))}
            </div>

            <Pagination
              total={products.length}
              current={currentPage}
              onChange={setCurrentPage}
            />
          </div>
        </div>
      </div>
    </div>
  );
}

export default ProductPage;
