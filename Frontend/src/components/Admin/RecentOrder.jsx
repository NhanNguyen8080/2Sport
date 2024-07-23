import { Fragment, useEffect, useState } from "react";
import {
    Card,
    Breadcrumbs,
    CardBody,
    Typography,
    Avatar,
    Checkbox,
    Collapse,
    Button,
} from "@material-tailwind/react";
import { fetchOrders } from "../../services/DashboardService";
import { useTranslation } from "react-i18next";
import { selectUser } from "../../redux/slices/authSlice";
import { useSelector } from "react-redux";
import { toast } from "react-toastify";


export default function RecentOrder() {
    const { t } = useTranslation();
    const [orders, setOrders] = useState([]);
    const [selectedRowKeys, setSelectedRowKeys] = useState([]);
    const [totalAmount, setTotalAmount] = useState(0);
    const [totalOrders, setTotalOrders] = useState(0);
    const user = useSelector(selectUser)

    const orderDetails = [
        { OrderId: 124, ProductName: "Vợt Cầu Lông Kumpoo Power Control K520 Pro Trắng", Quantity: 1, Price: 415000 },
        { OrderId: 125, ProductName: "Giày Nike Kyrie Flytrap 5 'White University Red' CZ41", Quantity: 1, Price: 415000 },
        { OrderId: 126, ProductName: "Giày Jordan Reveal Gym Red 2016", Quantity: 1, Price: 365000 },
        { OrderId: 128, ProductName: "Giày Adidas D Lillard 2.0 Away", Quantity: 1, Price: 300000 },
        { OrderId: 129, ProductName: "NBA CREW SOCKS", Quantity: 1, Price: 75000 },
        { OrderId: 130, ProductName: "NBA CREW SOCKS", Quantity: 1, Price: 75000 },
        { OrderId: 131, ProductName: "Giày Jordan Reveal Gym Red 2016", Quantity: 1, Price: 385000 },
        { OrderId: 132, ProductName: "NBA CREW SOCKS", Quantity: 1, Price: 95000 },
        { OrderId: 133, ProductName: "NBA CREW SOCKS", Quantity: 1, Price: 75000 },
        { OrderId: 137, ProductName: "Ống cầu 88", Quantity: 3, Price: 890000 },
        { OrderId: 138, ProductName: "Bóng Rổ Cao Su Số 7 Street Ball", Quantity: 1, Price: 1000000 },
        { OrderId: 139, ProductName: "BANH GERUSTAR FEDERATION", Quantity: 1, Price: 135000 },
        { OrderId: 141, ProductName: "Ống cầu 88", Quantity: 3, Price: 335000 }
    ];
    

    const [openOrderDetail, setOrderDetail] = useState(null);

    const toggleOpen = (orderId) => {
        console.log(orderId);
        setOrderDetail((cur) => (cur === orderId ? null : orderId));
    };

    const formatPrice = (value) => {
        return new Intl.NumberFormat('en-US', { minimumFractionDigits: 0 }).format(value) + ' VND';
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const ordersData = await fetchOrders();
                setOrders(ordersData);
                // console.log(`${t("dashboard.orders")}`, ordersData);

                // Calculate totals
                const totalOrdersCount = ordersData.length;
                const totalAmountSum = ordersData.reduce((acc, order) => acc + parseFloat(order.amount), 0);
                setTotalOrders(totalOrdersCount);
                setTotalAmount(totalAmountSum);
                toast.success("Orders fetched successfully");
            } catch (error) {
                console.log(error);
                setOrders([]);
            }
        };

        fetchData();
    }, []);

    const onSelectChange = (selectedKey) => {
        setSelectedRowKeys((prevSelectedRowKeys) =>
            prevSelectedRowKeys.includes(selectedKey)
                ? prevSelectedRowKeys.filter((key) => key !== selectedKey)
                : [...prevSelectedRowKeys, selectedKey]
        );
    };

    return (
        <>
            <Card className="h-screen w-[95.7%] mx-10 my-10">
                <Typography variant="h6" color="black" className="mx-10 mt-4 text-2xl">
                    {t("dashboard.recent_orders")}
                </Typography>

                <CardBody className="overflow-scroll px-0">
                    <table className="w-full min-w-max table-auto text-left">
                        <thead>
                            <tr>
                                <th className="border-y border-blue-gray-200 bg-blue-gray-200/50 p-4">
                                    <Checkbox
                                        color="blue"
                                        onChange={(e) => {
                                            if (e.target.checked) {
                                                setSelectedRowKeys(orders.map((row) => row.id));
                                            } else {
                                                setSelectedRowKeys([]);
                                            }
                                        }}
                                        checked={selectedRowKeys.length === orders.length}
                                        indeterminate={
                                            selectedRowKeys.length > 0 &&
                                            selectedRowKeys.length < orders.length
                                        }
                                    />
                                </th>
                                <th className="border-y border-blue-gray-100 bg-blue-gray-50/50 p-4">
                                    <Typography
                                        variant="large"
                                        color="blue-gray"
                                        className="font-normal leading-none opacity-70"
                                    >
                                        {t("dashboard.order_id")}
                                    </Typography>
                                </th>
                                <th className="border-y border-blue-gray-100 bg-blue-gray-50/50 p-4">
                                    <Typography
                                        variant="large"
                                        color="blue-gray"
                                        className="font-normal leading-none opacity-70"
                                    >
                                        {t("dashboard.date")}
                                    </Typography>
                                </th>
                                <th className="border-y border-blue-gray-100 bg-blue-gray-50/50 p-4">
                                    <Typography
                                        variant="large"
                                        color="blue-gray"
                                        className="font-normal leading-none opacity-70"
                                    >
                                        {t("dashboard.customer")}
                                    </Typography>
                                </th>
                                <th className="border-y border-blue-gray-100 bg-blue-gray-50/50 p-4">
                                    <Typography
                                        variant="large"
                                        color="blue-gray"
                                        className="font-normal leading-none opacity-70"
                                    >
                                        {t("dashboard.status")}
                                    </Typography>
                                </th>
                                <th className="border-y border-blue-gray-100 bg-blue-gray-50/50 p-4">
                                    <Typography
                                        variant="large"
                                        color="blue-gray"
                                        className="font-normal leading-none opacity-70"
                                    >
                                        {t("dashboard.total_price")}
                                    </Typography>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            {orders.map((order, index) => {
                                const isLast = index === orders.length - 1;
                                const classes = isLast
                                    ? "p-4"
                                    : "p-4 border-b border-blue-gray-50";
                                const isSelected = selectedRowKeys.includes(order.id);

                                const filteredOrderDetails = orderDetails.filter(detail => detail.OrderId === order.id);

                                return (
                                    <Fragment key={order.id}>
                                        <tr
                                            onClick={() => toggleOpen(order.id)}
                                            className={isSelected ? "bg-blue-100" : ""}
                                        >
                                            <td className={classes}>
                                                <Checkbox
                                                    color="blue"
                                                    checked={isSelected}
                                                    onChange={() => onSelectChange(order.id)}
                                                />
                                            </td>
                                            <td className={classes}>
                                                <Typography
                                                    variant="small"
                                                    color="blue-gray"
                                                    className="font-normal"
                                                >
                                                    {order.orderCode}
                                                </Typography>
                                            </td>
                                            <td className={classes}>
                                                <Typography
                                                    variant="small"
                                                    color="blue-gray"
                                                    className="font-normal"
                                                >
                                                    {new Date(order.createDate).toLocaleDateString()}
                                                </Typography>
                                            </td>
                                            <td className={classes}>
                                                <div className="flex items-center">
                                                    <Typography
                                                        variant="small"
                                                        color="blue-gray"
                                                        className="font-normal"
                                                    >
                                                        {order.customerName}
                                                    </Typography>
                                                </div>
                                            </td>
                                            <td className={classes}>
                                                <div className={`flex items-center justify-center  ${order.status === "PAID"
                                                    ? "text-green-500 border rounded-full w-fit px-2 border-green-500"
                                                    : order.status === "CANCELLED"
                                                        ? "text-red-500 border rounded-full w-fit px-2 border-red-500"
                                                        : order.status === "PROCESSING"
                                                            ? "text-orange-400 border rounded-full w-fit px-2 border-orange-400"
                                                            : "text-gray-500 border rounded-full w-fit px-2 border-gray-500"
                                                    }`}>
                                                    <Typography
                                                        variant="small"
                                                        className="font-normal"
                                                    >
                                                        {order.status}
                                                    </Typography>
                                                </div>
                                            </td>
                                            <td className={classes}>
                                                <Typography
                                                    variant="small"
                                                    color="blue-gray"
                                                    className="font-normal"
                                                >
                                                    {formatPrice(order.amount)}
                                                </Typography>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colSpan={6} className="p-0">
                                                <Collapse open={openOrderDetail === order.id}>
                                                    <div className="p-4 bg-gray-100 ">
                                                        {filteredOrderDetails.length > 0 ? (
                                                            filteredOrderDetails.map((detail) => (
                                                                <div key={detail.ProductName} className="mb-2 items-center justify-between pl-32">
                                                                    <Typography
                                                                        variant="small"
                                                                        color="blue-gray"
                                                                        className="font-normal"
                                                                    >
                                                                        <strong>Tên sản phẩm:</strong> {detail.ProductName}
                                                                    </Typography>
                                                                    <Typography
                                                                        variant="small"
                                                                        color="blue-gray"
                                                                        className="font-normal"
                                                                    >
                                                                        <strong>Số lượng:</strong> {detail.Quantity}
                                                                    </Typography>
                                                                    <Typography
                                                                        variant="small"
                                                                        color="blue-gray"
                                                                        className="font-normal"
                                                                    >
                                                                        <strong>Tổng thanh toán:</strong> {formatPrice(detail.Price)}
                                                                    </Typography>
                                                                </div>
                                                            ))
                                                        ) : (
                                                            <Typography
                                                                variant="small"
                                                                color="blue-gray"
                                                                className="font-normal"
                                                            >
                                                                Không có thông tin
                                                            </Typography>
                                                        )}
                                                    </div>
                                                </Collapse>
                                            </td>
                                        </tr>
                                    </Fragment>
                                );
                            })}
                        </tbody>
                    </table>
                </CardBody>
            </Card>
        </>
    );
}
