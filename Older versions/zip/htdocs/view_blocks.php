<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Original Contract Blocks</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            background-color: #f5f5f5;
        }
        .container {
            max-width: 1200px;
            margin: 0 auto;
            background-color: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        h1 {
            color: #333;
            border-bottom: 2px solid #4CAF50;
            padding-bottom: 10px;
        }
        .block {
            margin: 20px 0;
            padding: 15px;
            border: 1px solid #ddd;
            border-radius: 5px;
            background-color: #fafafa;
        }
        .block-header {
            font-weight: bold;
            color: #4CAF50;
            margin-bottom: 10px;
        }
        .block-info {
            margin: 5px 0;
            color: #666;
        }
        .block-text {
            margin: 10px 0;
            padding: 10px;
            background-color: white;
            border-left: 3px solid #4CAF50;
        }
        .block-image {
            margin: 10px 0;
            max-width: 100%;
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 5px;
        }
        .block-image img {
            max-width: 100%;
            height: auto;
        }
        .type-badge {
            display: inline-block;
            padding: 3px 8px;
            border-radius: 3px;
            font-size: 12px;
            font-weight: bold;
            margin-left: 10px;
        }
        .type-text {
            background-color: #2196F3;
            color: white;
        }
        .type-image {
            background-color: #FF9800;
            color: white;
        }
        .type-other {
            background-color: #9E9E9E;
            color: white;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>Original Contract Blocks</h1>
        
        <?php
        // Database connection
        $servername = "127.0.0.1";
        $username = "root";
        $password = "";
        $dbname = "kettera";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);

        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        // Set charset to UTF-8
        $conn->set_charset("utf8mb4");

        // Query to get all original contract blocks
        $sql = "SELECT 
                    ocb.Org_Cont_ID, 
                    ocb.Category_name, 
                    ocb.Contract_text, 
                    ocb.Created_by, 
                    ocb.Created_date, 
                    ocb.Type, 
                    ocb.MediaContent,
                    iu.First_name,
                    iu.Last_name
                FROM original_contract_block ocb
                LEFT JOIN internal_user iu ON ocb.Created_by = iu.Int_User_ID
                ORDER BY ocb.Org_Cont_ID DESC";

        $result = $conn->query($sql);

        if ($result->num_rows > 0) {
            // Output data for each row
            while($row = $result->fetch_assoc()) {
                echo '<div class="block">';
                
                // Block header with ID and type
                echo '<div class="block-header">';
                echo 'Block ID: ' . $row["Org_Cont_ID"];
                
                // Type badge
                $typeLabel = '';
                $typeClass = '';
                switch($row["Type"]) {
                    case 0:
                        $typeLabel = 'TEXT';
                        $typeClass = 'type-text';
                        break;
                    case 1:
                        $typeLabel = 'IMAGE';
                        $typeClass = 'type-image';
                        break;
                    default:
                        $typeLabel = 'OTHER';
                        $typeClass = 'type-other';
                }
                echo '<span class="type-badge ' . $typeClass . '">' . $typeLabel . '</span>';
                echo '</div>';
                
                // Block information
                echo '<div class="block-info">';
                echo '<strong>Category:</strong> ' . htmlspecialchars($row["Category_name"]) . '<br>';
                echo '<strong>Created by:</strong> ' . htmlspecialchars($row["First_name"]) . ' ' . htmlspecialchars($row["Last_name"]) . ' (ID: ' . $row["Created_by"] . ')<br>';
                echo '<strong>Created date:</strong> ' . $row["Created_date"];
                echo '</div>';
                
                // Display content based on type
                if ($row["Type"] == 1 && !empty($row["MediaContent"])) {
                    // Display image
                    echo '<div class="block-image">';
                    echo '<p><strong>Image Content:</strong></p>';
                    // Convert BLOB to base64 and display
                    $imageData = base64_encode($row["MediaContent"]);
                    echo '<img src="data:image/jpeg;base64,' . $imageData . '" alt="Contract Block Image">';
                    echo '</div>';
                    
                    // Also show text if any
                    if (!empty($row["Contract_text"])) {
                        echo '<div class="block-text">';
                        echo '<strong>Text:</strong><br>';
                        echo nl2br(htmlspecialchars($row["Contract_text"]));
                        echo '</div>';
                    }
                } else {
                    // Display text content
                    echo '<div class="block-text">';
                    if (!empty($row["Contract_text"])) {
                        echo nl2br(htmlspecialchars($row["Contract_text"]));
                    } else {
                        echo '<em>(No text content)</em>';
                    }
                    echo '</div>';
                }
                
                echo '</div>'; // Close block div
            }
        } else {
            echo '<p>No contract blocks found.</p>';
        }

        $conn->close();
        ?>
        
    </div>
</body>
</html>