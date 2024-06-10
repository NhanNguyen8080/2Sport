import { useState, useEffect } from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
  faTableCellsLarge,
  faBars,
  faXmark
} from '@fortawesome/free-solid-svg-icons';
import PriceRangeSlider from "../components/Product/PriceRangeSlider ";
import ProductList from "./ProductList";
import { fetchBrands } from '../services/brandService';
import { fetchCategories } from '../services/categoryService';
import { useSelector } from 'react-redux';
import { selectProducts } from '../redux/slices/productSlice';

function ProductPage() {
  const [brands, setBrands] = useState([]);
  const [categories, setCategories] = useState([]);
  const [filters, setFilters] = useState({
    sortBy: '',
    size: [],
    minPrice: 0,
    maxPrice: 5000000,
    brandId: [],
    categoryId: [],
    classificationId: []
  });
  const products = useSelector(selectProducts);

  useEffect(() => {
    const getBrands = async () => {
      try {
        const brandsData = await fetchBrands();
        setBrands(brandsData);
      } catch (error) {
        console.error('Error fetching brand data:', error);
      }
    };

    getBrands();
  }, []);

  useEffect(() => {
    const getCategories = async () => {
      try {
        const categoriesData = await fetchCategories();
        setCategories(categoriesData);
      } catch (error) {
        console.error('Error fetching category data:', error);
      }
    };

    getCategories();
  }, []);

  const handleSortChange = (e) => {
    setFilters((prevFilters) => ({ ...prevFilters, sortBy: e.target.value }));
  };

  const handleSizeChange = (size) => {
    setFilters((prevFilters) => ({
      ...prevFilters,
      size: prevFilters.size.includes(size)
        ? prevFilters.size.filter((s) => s !== size)
        : [...prevFilters.size, size]
    }));
  };

  const handleBrandChange = (brandId) => {
    setFilters((prevFilters) => ({
      ...prevFilters,
      brandId: prevFilters.brandId.includes(brandId)
        ? prevFilters.brandId.filter((id) => id !== brandId)
        : [...prevFilters.brandId, brandId]
    }));
  };

  const handleCategoryChange = (categoryId) => {
    setFilters((prevFilters) => ({
      ...prevFilters,
      categoryId: prevFilters.categoryId.includes(categoryId)
        ? prevFilters.categoryId.filter((id) => id !== categoryId)
        : [...prevFilters.categoryId, categoryId]
    }));
  };

  const handleClassificationChange = (classificationId) => {
    setFilters((prevFilters) => ({
      ...prevFilters,
      classificationId: prevFilters.classificationId.includes(classificationId)
        ? prevFilters.classificationId.filter((id) => id !== classificationId)
        : [...prevFilters.classificationId, classificationId]
    }));
  };

  const handleClearFilters = () => {
    setFilters({
      sortBy: '',
      size: [],
      minPrice: 0,
      maxPrice: 1000,
      brandId: [],
      categoryId: [],
      classificationId: []
    });
  };

  return (
    <div className="">
      <div className="w-full px-20">
        <div className="grid grid-cols-1 lg:grid-cols-4 gap-4 mt-2">
          <div className="w-full lg:col-span-1">
            <div className=" w-full">
              <div className=" mb-4 font-bold text-3xl">
                Products
              </div>
              <div>
                <div>
                  <input type="checkbox" onChange={() => handleClassificationChange(1)} />
                  <label>new products</label>
                </div>
                <div>
                  <input type="checkbox" onChange={() => handleClassificationChange(2)} />
                  <label>2hand products</label>
                </div>
              </div>
              
              <div className="h-px bg-gray-300 my-5 mx-auto"></div>
              <div className="Products text-black font-bold">Categories</div>
              <div className=" relative p-4">
                <div className="grid grid-cols-1 gap-2">
                  {categories.map((category, index) => (
                    <label key={index} className="inline-flex items-center">
                      <input
                        type="checkbox"
                        className="form-checkbox h-5 w-5 text-orange-500"
                        value={category.categoryName}
                        onChange={() => handleCategoryChange(category.id)}
                      />
                      <span className="ml-2 text-black">{category.categoryName}</span>
                    </label>
                  ))}
                </div>
              </div>
              
              <div className="h-px bg-gray-300 my-5 mx-auto"></div>
              <div className="Products text-black font-bold">Brand</div>
              <div className=" relative p-4">
                <div className="grid grid-cols-1 gap-2">
                  {brands.map((brand, index) => (
                    <label key={index} className="inline-flex items-center">
                      <input
                        type="checkbox"
                        className="form-checkbox h-5 w-5 text-orange-500"
                        value={brand.brandName}
                        onChange={() => handleBrandChange(brand.id)}
                      />
                      <span className="ml-2 text-black">{brand.brandName} ({brand.quantity})</span>
                    </label>
                  ))}
                </div>
              </div>
              
              <div className="h-px bg-gray-300 my-5 mx-auto"></div>
              <div className="Products text-black font-bold">Price</div>
              <div className="p-4">
                <PriceRangeSlider
                  min={filters.minPrice}
                  max={filters.maxPrice}
                  onMinChange={(value) => setFilters((prevFilters) => ({ ...prevFilters, minPrice: value }))}
                  onMaxChange={(value) => setFilters((prevFilters) => ({ ...prevFilters, maxPrice: value }))}
                />
              </div>
              
              <div className="h-px bg-gray-300 my-5 mx-auto"></div>
              <div className="Products text-black font-bold">Size</div>
              <div className="relative p-4">
                <div>
                  <label>
                    <input
                      type="checkbox"
                      onChange={() => handleSizeChange(1)}
                    />
                    <span>S</span>
                  </label>
                </div>
                <div>
                  <label>
                    <input
                      type="checkbox"
                      onChange={() => handleSizeChange(2)}
                    />
                    <span>M</span>
                  </label>
                </div>
                <div>
                  <label>
                    <input
                      type="checkbox"
                      onChange={() => handleSizeChange(3)}
                    />
                    <span>L</span>
                  </label>
                </div>
              </div>
            </div>
            <div className="flex justify-between mt-4">
              <button onClick={handleClearFilters}>
                <FontAwesomeIcon icon={faXmark} />
              </button>
              <div>
                <button className="p-2 border rounded-lg">Save Filter</button>
              </div>
            </div>
          </div>
          <div className="col-span-3">
            <div className="w-full">
              <div className="grid grid-cols-2 lg:grid-cols-4 gap-4 items-center">
                <div className="w-full col-span-1 lg:col-span-1">
                  <label htmlFor="sort-select" className="block text-sm font-medium text-gray-700">Sort by:</label>
                  <select
                    id="sort-select"
                    className="mt-1 block w-full py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                    onChange={handleSortChange}
                  >
                    <option value="lowest-price">Price: Lowest to Highest</option>
                    <option value="highest-price">Price: Highest to Lowest</option>
                    <option value="new-arrivals">New Arrivals</option>
                  </select>
                </div>
                <div className="w-full col-span-1 lg:col-span-3 flex justify-end">
                  <button className="w-8 h-8 border border-gray-400 rounded-full flex items-center justify-center mr-2">
                    <FontAwesomeIcon icon={faTableCellsLarge} />
                  </button>
                  <button className="w-8 h-8 border border-gray-400 rounded-full flex items-center justify-center">
                    <FontAwesomeIcon icon={faBars} />
                  </button>
                </div>
              </div>
              <ProductList filters={filters} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ProductPage;
